using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IClientSaveComposerService
    {
        IEnumerable<IMedia> GetFolder<T>(T item, int folderId);
        IEnumerable<IMedia> GetFolder<T>(T item, string folderName, int folderId);
        IRelation CheckMediaRelation(IMedia media, IContent publishedItem, int id);
        void MoveOrCreate(IRelation mediaRelation, IMedia clientMedia, IMedia clientMediaFolder, IContent publishedItem, string mediaType, Property property);
        bool LookForDirtyProperties<T>(T publishedItem);
        IEnumerable<Property> GetMedia(IContent publishedItem, int id1, int id2);
        string GetMediaPath(string jsonValues);
        IEnumerable<Property> GetProperties(IContent publishedItem, int dataTypeId); 
        void CreateUserGroups(IContent publishedItem, IMedia folder);
        void OnClientPublishEvent(IMedia folder, IContent publishedItem, UmbracoHelper helper);
        void CreateMemberGroups(IContent publishedItem);

    }
}
