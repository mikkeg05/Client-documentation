using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Umbraco.Core.Cache;
using Umbraco.Core.Dictionary;
using Umbraco.Core.Mapping;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.Security.Providers;
using Umbraco.Web.Security;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using ClientDocumentation.Web.Business.Interfaces;
using System.Runtime.CompilerServices;
using ClientDocumentation.Web.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;
using System.Collections.Specialized;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Configuration;
using Umbraco.Web.Routing;

namespace ClientDocumentation.Web.Test
{
    public abstract class UmbracoBaseTest
    {
        public ServiceContext ServiceContext;
        public MembershipHelper MembershipHelper;
        public UmbracoHelper UmbracoHelper;
        public UmbracoMapper UmbracoMapper;

        public Mock<IUserService> UserService;
        public Mock<IMyUserService> MyUserService;
        public Mock<IClientSaveComposerService> MediaComposerService;
        public Mock<IMemberSignUpService> MemberSignupService;
        public Mock<IContentService> ContentService;
        public Mock<IMediaService> MediaService;
        public Mock<IRelationService> RelationService;
        public Mock<IClientService> ClientService;

        public Mock<ICultureDictionary> CultureDictionary;
        public Mock<ICultureDictionaryFactory> CultureDictionaryFactory;
        public Mock<IPublishedContentQuery> PublishedContentQuery;

        public Mock<HttpContextBase> HttpContext;
        public Mock<IMemberService> memberService;
        public Mock<IPublishedMemberCache> memberCache;

        [SetUp]
        public virtual void SetUp()
        {
            SetupUserServices();
            SetupMediaComposerService();
            SetupContentService();
            SetupMediaService();
            SetupRelationService();
            SetupClientService();
            SetupMemberSignupService();
            this.SetupHttpContext();
            this.SetupCultureDictionaries();
            this.SetupPublishedContentQuerying();
            this.SetupMembership();

            this.ServiceContext = ServiceContext.CreatePartial();
            this.UmbracoHelper = new UmbracoHelper(Mock.Of<IPublishedContent>(), Mock.Of<ITagQuery>(), this.CultureDictionaryFactory.Object, Mock.Of<IUmbracoComponentRenderer>(), this.PublishedContentQuery.Object, this.MembershipHelper);
            //this.UmbracoMapper = new UmbracoMapper(new MapDefinitionCollection(new List<IMapDefinition>()));
        }

        public virtual void SetupHttpContext()
        {
            this.HttpContext = new Mock<HttpContextBase>();
        }
        public virtual void SetupMemberSignupService() 
        {
            MemberSignupService = new Mock<IMemberSignUpService>();
        }
        public virtual void SetupCultureDictionaries()
        {
            this.CultureDictionary = new Mock<ICultureDictionary>();
            this.CultureDictionaryFactory = new Mock<ICultureDictionaryFactory>();
            this.CultureDictionaryFactory.Setup(x => x.CreateDictionary()).Returns(this.CultureDictionary.Object);
        }
        public virtual void SetupMediaComposerService()
        {
            MediaComposerService = new Mock<IClientSaveComposerService>();
        }
        public virtual void SetupPublishedContentQuerying()
        {
            this.PublishedContentQuery = new Mock<IPublishedContentQuery>();
        }
        public virtual void SetupUserServices() { MyUserService = new Mock<IMyUserService>(); UserService = new Mock<IUserService>(); }
        public virtual void SetupContentService()
        {
            ContentService = new Mock<IContentService>();
        }
        public virtual void SetupClientService() { ClientService = new Mock<IClientService>(); }
        public virtual void SetupMediaService()
        {
            MediaService = new Mock<IMediaService>();
        }
        public virtual void SetupRelationService()
        {
            RelationService = new Mock<IRelationService>();
        }
        public virtual void SetupMembership()
        {
            this.memberService = new Mock<IMemberService>();
            var memberTypeService = Mock.Of<IMemberTypeService>();
            var membershipProvider = new MembersMembershipProvider(memberService.Object, memberTypeService);

            this.memberCache = new Mock<IPublishedMemberCache>();
            this.MembershipHelper = new MembershipHelper(this.HttpContext.Object, this.memberCache.Object, membershipProvider, Mock.Of<RoleProvider>(), memberService.Object, memberTypeService, Mock.Of<IUserService>(), Mock.Of<IPublicAccessService>(), AppCaches.NoCache, Mock.Of<ILogger>());
        }
        public virtual void SetupSearchService() 
        { 
            
        }

        public void SetupPropertyValue(Mock<IPublishedContent> publishedContentMock, string alias, object value, string culture = null, string segment = null)
        {
            var property = new Mock<IPublishedProperty>();
            property.Setup(x => x.Alias).Returns(alias);
            property.Setup(x => x.GetValue(culture, segment)).Returns(value);
            property.Setup(x => x.HasValue(culture, segment)).Returns(value != null);
            publishedContentMock.Setup(x => x.GetProperty(alias)).Returns(property.Object);
        }
        public void SetupContentPropertyValue(Mock<IContent> contentMock, string alias, object value, string culture = null, string segment = null) 
        {
            var property = new Mock<IPublishedProperty>();
            property.Setup(x => x.Alias).Returns(alias);
            property.Setup(x => x.GetValue(culture, segment)).Returns(value);
            property.Setup(x => x.HasValue(culture, segment)).Returns(value != null);
            var s = contentMock.Setup(x => x.GetValue(alias, culture, segment, false)).Returns(property.Object);
        }
        public static void SetFakeContext(Controller controller) 
        {
            var httpContext = MakeFakeContext();
            ControllerContext context =
            new ControllerContext(
            new RequestContext(httpContext,
            new RouteData()), controller);
            controller.ControllerContext = context;
        }
        private static HttpContextBase MakeFakeContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);
            context.Setup(c => c.Session).Returns(session.Object);
            context.Setup(c => c.Server).Returns(server.Object);
            context.Setup(c => c.User).Returns(user.Object);
            user.Setup(c => c.Identity).Returns(identity.Object);
            identity.Setup(i => i.IsAuthenticated).Returns(true);
            identity.Setup(i => i.Name).Returns("admin");

            return context.Object;
        }
        public UmbracoContext GetContext() 
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.QueryString).Returns(new NameValueCollection());

            this.HttpContext.Setup(x => x.Request).Returns(request.Object);
            var _umbracoContextFactory = new UmbracoContextFactory(
            Mock.Of<IUmbracoContextAccessor>(),
            Mock.Of<IPublishedSnapshotService>(),
            Mock.Of<IVariationContextAccessor>(),
            Mock.Of<IDefaultCultureAccessor>(),
            new UmbracoSettingsSection(),
            Mock.Of<IGlobalSettings>(),
            new UrlProviderCollection(new IUrlProvider[] { Mock.Of<IUrlProvider>() }),
            new MediaUrlProviderCollection(new IMediaUrlProvider[] { Mock.Of<IMediaUrlProvider>() }),
            Mock.Of<IUserService>()
            );
            return _umbracoContextFactory.EnsureUmbracoContext(this.HttpContext.Object).UmbracoContext;
        }

    }
}