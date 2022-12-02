using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;
using static Umbraco.Core.Constants.Conventions;

namespace ClientDocumentation.Web.Business.Services
{
    public class ClientSaveComposerService : IClientSaveComposerService
    {
        private readonly IPublicAccessService _publicAccessService;
        private readonly IMemberGroupService _memberGroupService;
        private readonly JsonHelper _jsonHelper;
        private readonly IUserService _userService;
        private readonly IContentService _contentService;
        private readonly WebClient _client = new WebClient();
        private readonly IMediaService _mediaService;
        private readonly IRelationService _relationService;
        private readonly ILogger _logger;
        public ClientSaveComposerService
           (IMediaService mediaService, IRelationService relationService, 
            IContentService contentService, IUserService userService, 
            IMemberGroupService memberGroupService, 
            IPublicAccessService publicAccessService, ILogger logger)
        {
            _logger = logger;
            _mediaService = mediaService;
            _relationService = relationService;
            _contentService = contentService;
            _userService = userService;
            _jsonHelper = new JsonHelper();
            _memberGroupService = memberGroupService;
            _publicAccessService = publicAccessService;
        }

        public IEnumerable<IMedia> GetFolder<T>(T item, int folderId)
        {
            const int pageSize = 500;
            var total = long.MaxValue;
            if (typeof(T) == typeof(IMedia))
            {
                var media = (IMedia)item;
                _logger.Info<ClientSaveComposerService>($"Umbraco found a media object: {media.Name}");
                return _mediaService.GetPagedChildren(media.Id, 0, pageSize, out total);
            }
            if (typeof(T) == typeof(IContent))
            {
                var content = (IContent)item;
                
                _logger.Info<ClientSaveComposerService>($"Umbraco found a media object: {content.Name}");
                return _mediaService.GetPagedChildren(folderId, 0, pageSize, out total).Where(x => x.Name == content.Name);
            }
            return null;
        }
        public IEnumerable<IContent> GetContent(IContent content, string childName)
        {
            const int pageSize = 500;
            var total = long.MaxValue;
            return _contentService.GetPagedChildren(content.Id, 0, pageSize, out total).Where(x => x.Name == childName);
        }
        public IEnumerable<IMedia> GetFolder<T>(T item, string folderName, int folderId)
        {
            const int pageSize = 500;
            var total = long.MaxValue;
            if (typeof(T) == typeof(IMedia))
            {
                var media = (IMedia)item;
                return _mediaService.GetPagedChildren(media.Id, 0, pageSize, out total).Where(x => x.Name == folderName);
            }
            if (typeof(T) == typeof(IContent))
            {
                var content = (IContent)item;
                return _mediaService.GetPagedChildren(folderId, 0, pageSize, out total).Where(x => x.Name == content.Name);
            }
            return null;
        }
        public IRelation CheckMediaRelation(IMedia media, IContent publishedItem, int id)
        {
            var imageRelations = _relationService.GetByChildId(media.Id);
            IRelation imageRelation = imageRelations.FirstOrDefault(x => x.RelationTypeId == id);
            if (imageRelation != null)
            {
                return imageRelation;
            }
            return null;
        }
        public IRelation CheckMediaRelation(IPublishedContent media, IContent publishedItem)
        {
            var imageRelations = _relationService.GetByChildId(media.Id);
            IRelation imageRelation = imageRelations.FirstOrDefault(x => x.RelationTypeId == 10);
            if (imageRelation != null)
            {
                return imageRelation;
            }
            return null;
        }

        public void MoveOrCreate(IRelation mediaRelation, IMedia clientMedia, IMedia clientMediaFolder, IContent publishedItem, string mediaType, Property property)
        {
            if (mediaRelation == null)
            {
                _mediaService.Move(clientMedia, clientMediaFolder.Id);
                _relationService.Relate(publishedItem.Id, clientMedia.Id, "clientMedia");
            }
            else if (mediaRelation.ParentId != publishedItem.Id)
            {

                string fileType = clientMedia.GetValue("umbracoExtension").ToString();
                string filePath = HttpContext.Current.Server.MapPath(GetMediaPath(clientMedia.GetValue("umbracoFile").ToString()));
                byte[] bytes = _client.DownloadData(filePath);
                var newImageMedia = _mediaService.CreateMedia(clientMedia.Name, clientMediaFolder.Id, mediaType);

                using (Stream stream = new MemoryStream(bytes))
                {
                    newImageMedia.SetValue(Umbraco.Core.Composing.Current.Services.ContentTypeBaseServices, Constants.Conventions.Media.File, clientMedia.Name + "." + fileType, stream);
                }

                _mediaService.Save(newImageMedia);
                _relationService.Relate(publishedItem.Id, newImageMedia.Id, "clientMedia");
                publishedItem.SetValue(property.Alias, newImageMedia.GetUdi().ToString());
            }
        }
        public bool LookForDirtyProperties<T>(T publishedItem)
        {
            
            if(typeof(T) == typeof(IContent)) 
            {
                var publishedContent = (IContent)publishedItem;
                foreach (var prop in publishedContent.Properties)
                {
                    if (prop.IsDirty())
                    {
                        return true;
                    }
                }
            }
            if(typeof (T) == typeof(IPublishedContent)) 
            {
                var publishedContent = (IPublishedContent)publishedItem;
                if(publishedContent.Properties.Select(x => x.TryConvertTo<Property>().Success).Any()) 
                { 
                    foreach(var prop in publishedContent.Properties) 
                    {
                        var property = (Property)prop;
                        if (property.IsDirty()) { return true; }
                    }
                }
            }

            return false;
        }


        public IEnumerable<Property> GetMedia(IContent publishedItem, int id1, int id2)
        {
            var mediaPicker1 = GetProperties(publishedItem, id1);
            var mediaPicker2 = GetProperties(publishedItem, id2);

            List<Property> result = new List<Property>();
            foreach (var item in mediaPicker1) { result.Add(item); }
            foreach (var item in mediaPicker2) { result.Add(item); }
            IEnumerable<Property> results = result;
            return results;
        }

        public string GetMediaPath(string jsonValues)
        {
            if (JsonConvert.DeserializeObject<MediaPath>(jsonValues) == null)
            {
                return null;
            }
            MediaPath mediaPath = JsonConvert.DeserializeObject<MediaPath>(jsonValues);

            return mediaPath.Path;
        }


        public IEnumerable<Property> GetProperties(IContent publishedItem, int dataTypeId)
        {
            return publishedItem.Properties.Where(x => x.PropertyType.DataTypeId == dataTypeId);
        }
        public void CreateMemberGroups(IContent publishedItem)
        {
            if (_memberGroupService.GetByName(publishedItem.Name) != null) { return; }
            MemberGroup newMemberGroup = new MemberGroup();
            newMemberGroup.Name = publishedItem.Name;
            _memberGroupService.Save(newMemberGroup);
        }
        public void CreateUserGroups(IContent publishedItem, IMedia mediaFolder)
        {
            if (publishedItem == null) { return; }

            var userValues = System.Text.RegularExpressions.Regex.Unescape(publishedItem.GetValue(publishedItem.Properties.
                FirstOrDefault(x => x.PropertyType.DataTypeId == 1077).Alias).ToString().Split(new string[] { "teamMembers\":\"" }, StringSplitOptions.None).Last().Split(']')[0] + "]");


            List<IUser> existingUsers = new List<IUser>();
            if (userValues == null) { return; }

            var teamMembers = _jsonHelper.GenericDeserializer<TeamMembers>(userValues);

            List<int> userIds = new List<int>();
            foreach (var item in teamMembers)
            {
                userIds.Add(item.User);
            }
            UserGroup users = new UserGroup
            {
                Alias = publishedItem.Name,
                Name = publishedItem.Name
            };
            users.AddAllowedSection("content");
            users.AddAllowedSection("media");
            users.StartContentId = publishedItem.Id;
            if (mediaFolder != null) { users.StartMediaId = mediaFolder.Id; }
            else { users.StartMediaId = -1; }
            users.Permissions = new List<string> { "I", "P", "K", "F", "ï", "D", "C", "U", "R", "H", "Z", "A", "O", "S" };

            var existingUserGroup = _userService.GetUserGroupByAlias(users.Alias);
            if (!userIds.Any() && existingUserGroup == null)
            {
                _userService.Save(users);
                return;
            }
            //foreach (var item in publishedItem.Properties.Where(x => x.PropertyType.DataTypeId == 1077))
            //{
            //    userIds.Add(item.Id);
            //}
            if (existingUserGroup != null)
            {
                _userService.Save(existingUserGroup, userIds.ToArray());
                foreach (var user in userIds) { _userService.Save(_userService.GetUserById(user)); }
                return;
            }
            _userService.Save(users, userIds.ToArray());
            foreach (var user in userIds) { _userService.Save(_userService.GetUserById(user)); }
        }


        public void ContentServicePublished(IMedia folder, IContent publishedItem, int contentTypeId, UmbracoHelper helper)
        {

            var existingFolder = GetFolder(publishedItem, folder.Id);
            IMedia newMedia = _mediaService.CreateMedia(publishedItem.Name, folder.Id, "Folder");
            if (!existingFolder.Any())
            {
                _mediaService.Save(newMedia);
            }
            existingFolder = GetFolder(publishedItem, folder.Id);
            var relations = _relationService.GetByParentId(publishedItem.Id);
            IRelation relation = relations.FirstOrDefault(x => x.RelationTypeId == 11);
            if (relation == null)
            {
                _relationService.Relate(publishedItem.Id, existingFolder.FirstOrDefault().Id, "clientFolder");
            }
            if (existingFolder != null && !GetFolder(existingFolder.FirstOrDefault(), folder.Id).Any())
            {
                IMedia imagesFolder = _mediaService.CreateMedia("Images", existingFolder.FirstOrDefault().Id, "Folder");
                IMedia videoFolder = _mediaService.CreateMedia("Videos", existingFolder.FirstOrDefault().Id, "Folder");
                List<IMedia> subfolders = new List<IMedia>();
                subfolders.Add(imagesFolder);
                subfolders.Add(videoFolder);
                _mediaService.Save(subfolders.AsEnumerable());
            }
            if (GetFolder(existingFolder.FirstOrDefault(), folder.Id).Where(x => x.Name == "Images").Any())
            {
                relations = _relationService.GetByParentId(publishedItem.Id);
                relation = relations.FirstOrDefault(x => x.RelationTypeId == 11);
                if (relation != null)
                {
                    var clientMediaFolder = _mediaService.GetById(relation.ChildId);
                    CreateUserGroups(publishedItem, clientMediaFolder);
                    var clientImageFolder = GetFolder(clientMediaFolder, "Images", folder.Id).FirstOrDefault();
                    var clientVideoFolder = GetFolder(clientMediaFolder, "Videos", folder.Id).FirstOrDefault();
                    var mediaPicker = GetProperties(publishedItem, 1051);

                    var imagePickers = GetMedia(publishedItem, 1053, 1054);

                    if (imagePickers.Any())
                    {
                        foreach (var prop in imagePickers)
                        {

                            if (prop != null && prop.Values != null && prop.Values.Any())
                            {
                                if (prop.Values.Any())
                                {
                                    var value = publishedItem.GetValue(prop.Alias);
                                    if (value.ToString().Contains("mediaKey"))
                                    {
                                        List<KeyMediaKey> keyMedias = _jsonHelper.GenericDeserializer<KeyMediaKey>(publishedItem.GetValue(prop.Alias).ToString());
                                        List<Guid> mediaKeys = new List<Guid>();
                                        foreach (var item in keyMedias) { mediaKeys.Add(item.MediaKey); }
                                        if (mediaKeys.Any())
                                        {
                                            foreach (var mediaKey in mediaKeys)
                                            {
                                                var clientImage = _mediaService.GetById(mediaKey);
                                                if (clientImage != null)
                                                {
                                                    IRelation mediaRelation = CheckMediaRelation(clientImage, publishedItem, 10);
                                                    MoveOrCreate(mediaRelation, clientImage, clientImageFolder, publishedItem, "Image", prop);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        IPublishedContent mediaItem = helper.Media(value);
                                        if (mediaItem != null)
                                        {
                                            var clientImage = _mediaService.GetById(mediaItem.Id);
                                            if (clientImage != null)
                                            {
                                                IRelation mediaRelation = CheckMediaRelation(clientImage, publishedItem, 10);
                                                MoveOrCreate(mediaRelation, clientImage, clientImageFolder, publishedItem, "Image", prop);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (mediaPicker.Any())
                    {
                        foreach (var prop in mediaPicker)
                        {
                            if (prop != null && prop.Values != null)
                            {
                                var value = publishedItem.GetValue(prop.Alias);
                                if (value.ToString().Contains("mediaKey"))
                                {
                                    List<KeyMediaKey> keyMedias = _jsonHelper.GenericDeserializer<KeyMediaKey>(publishedItem.GetValue(prop.Alias).ToString());
                                    List<Guid> mediaKeys = new List<Guid>();
                                    foreach (var item in keyMedias) { mediaKeys.Add(item.MediaKey); }
                                    if (mediaKeys.Any())
                                    {
                                        foreach (var mediaKey in mediaKeys)
                                        {
                                            var clientVideo = _mediaService.GetById(mediaKey);
                                            if (clientVideo != null)
                                            {
                                                IRelation mediaRelation = CheckMediaRelation(clientVideo, publishedItem, 10);
                                                MoveOrCreate(mediaRelation, clientVideo, clientVideoFolder, publishedItem, "umbracoMediaVideo", prop);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    IPublishedContent mediaItem = helper.Media(value);
                                    if (mediaItem != null)
                                    {
                                        var clientVideo = _mediaService.GetById(mediaItem.Id);
                                        if (clientVideo != null)
                                        {
                                            IRelation mediaRelation = CheckMediaRelation(clientVideo, publishedItem, 10);
                                            MoveOrCreate(mediaRelation, clientVideo, clientVideoFolder, publishedItem, "umbracoMediaVideo", prop);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CreateMemberGroups(publishedItem);
            if (publishedItem.Properties.FirstOrDefault(x => x.PropertyType.DataTypeId == -49).GetValue().TryConvertTo<int>().Result == 1)
            {
                var rule = Current.Services.PublicAccessService.GetEntryForContent(publishedItem);

                if (rule == null)
                {
                    var homePage = _contentService.GetRootContent().FirstOrDefault();
                    var loginPage = GetContent(homePage, "Loginpage").FirstOrDefault();
                    var noAccess = GetContent(homePage, "No access").FirstOrDefault();

                    if (homePage != null && loginPage != null && noAccess != null)
                    {
                        var newRule = new PublicAccessEntry(publishedItem, loginPage, noAccess, new List<PublicAccessRule>());
                        var memberGroup = _memberGroupService.GetByName(publishedItem.Name);
                        newRule.AddRule(memberGroup.Name, Umbraco.Core.Constants.Conventions.PublicAccess.MemberRoleRuleType);
                        _publicAccessService.Save(newRule);
                    }
                }
            }
            if (LookForDirtyProperties<IContent>(publishedItem))
            {
                _contentService.Save(publishedItem);
            }
        }
    }
}