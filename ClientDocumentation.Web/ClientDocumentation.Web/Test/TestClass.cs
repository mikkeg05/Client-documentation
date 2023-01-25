using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using ClientDocumentation.Web.Controllers;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace ClientDocumentation.Web.Test
{
    [TestFixture]
    public class TestClass : UmbracoBaseTest
    {

        Mock<IContentService> contentServiceMock = new Mock<IContentService>();
        Mock<IContentType> contentClient = new Mock<IContentType>();
        private Mock<IPublishedContent> content;


        [OneTimeSetUp]
        public void SetUpOnce()
        {
            base.SetUp();
        }

        [SetUp]
        public override void SetUp()
        {
            Current.Factory = new Mock<IFactory>().Object;
            content = new Mock<IPublishedContent>();



        }
        [TearDown]
        public void TearDown()
        {
            Current.Reset();
        }

        [Test]
        public void TestSetPropertyValue()
        {
            var publishedContent = new Mock<IPublishedContent>();
            base.SetupPropertyValue(publishedContent, "isHidden", true);
            var model = new Client(publishedContent.Object);
            Assert.AreEqual(true, model.IsHidden);
            base.SetupPropertyValue(publishedContent, "isHidden", false);
            var model2 = new Client(publishedContent.Object);
            Assert.AreNotEqual(true, model2.IsHidden);
        }
        [Test]
        [TestCase("testClient", "testClient")]
        [TestCase("", "")]
        [TestCase("Med mellemrum", "Med mellemrum")]
        public void TestCreateClient(string value, string expected)
        {
            Mock<IContent> clientMock = new Mock<IContent>();
            ClientService.Setup(x => x.CreateClient(value)).Returns(clientMock.Object);
            clientMock.Setup(x => x.Name).Returns(value);
            var testClient = ClientService.Object.CreateClient("testClient");


            Assert.AreEqual(clientMock.Object.Name, expected);
            Assert.IsNotNull(testClient);
        }

        [Test]
        public void TestNegativeCreateClient()
        {
            Mock<IContent> clientMock = new Mock<IContent>();
            ClientService.Setup(x => x.CreateClient(null)).Returns(value: null);

            Assert.IsNull(ClientService.Object.CreateClient(null));
        }

    }
    [TestFixture(typeof(SearchService))]
    public class SearchControllerTest : UmbracoBaseTest
    {
        private SearchController controller;
        Mock<ISearchService> searchServiceMock;
        private ISearchService _searchService;
        UmbracoContext context;
        private ISearchService searchService;

        public SearchControllerTest(Type t) : this((ISearchService)Activator.CreateInstance(t)) { }

        public SearchControllerTest(ISearchService searchService)
        {
            this.searchService = searchService;
        }


        //public SearchControllerTest(ISearchService searchService) : this(_searchService) { }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            //Current.Factory = new Mock<IFactory>().Object;
            searchServiceMock = new Mock<ISearchService>();
            this.controller = new SearchController(searchServiceMock.Object, Mock.Of<IGlobalSettings>(), Mock.Of<IUmbracoContextAccessor>(), base.ServiceContext, AppCaches.NoCache, Mock.Of<IProfilingLogger>(), base.UmbracoHelper);
        }

        [Test]
        [TestCase("ois", 2)]
        public void TestSearch(string value, int expected)
        {
            Mock<IPublishedContent> clientMock1 = new Mock<IPublishedContent>();
            clientMock1.Setup(x => x.Name).Returns("oister");

            Mock<IPublishedContent> clientMock2 = new Mock<IPublishedContent>();
            clientMock2.Setup(x => x.Name).Returns("pirateroist");


            var umbContext = base.GetContext();
            umbContext.HttpContext.Request.QueryString["q"] = value;
            var contentModel = new Mock<ContentModel>(new Mock<IPublishedContent>().Object);
            Search search = new Search(contentModel.Object.Content);
            PublishedSearchResult[] results =
            {
                new PublishedSearchResult(clientMock1.Object, 2),
                new PublishedSearchResult(clientMock2.Object, 2)
            };
            List<PublishedSearchResult> resultList = results.ToList();
            SearchPageViewModel searchViewModel = new SearchPageViewModel(search) { SearchResults = resultList };
            searchServiceMock.Setup(x => x.GetSearchPageViewModel(search, umbContext.HttpContext.Request, umbContext)).Returns(searchViewModel);
            var newSearchModel = searchServiceMock.Object.GetSearchPageViewModel(search, umbContext.HttpContext.Request, umbContext);

            Assert.AreEqual(expected, newSearchModel.ResultCount);
            Assert.AreEqual(clientMock1.Object.Name, newSearchModel.SearchResults.FirstOrDefault().Content.Name);
            searchServiceMock.Verify();

        }
    }
    [TestFixture]
    public class TestMemberViewModel : UmbracoBaseTest
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }
        [Test]
        [TestCase("mail@mail.dk", "mikkel :)")]
        [TestCase("mail@mail.dk", "")]
        public void TestMemberSignup(string email, string userName)
        {
            var _memberSignUpService = A.Fake<IMemberSignUpService>();
            Mock<IUser> userMock = new Mock<IUser>();
            Mock<IMember> memberMock = new Mock<IMember>();

            userMock.Setup(x => x.Email).Returns(email);
            userMock.Setup(x => x.Username).Returns(userName);

            memberMock.Setup(x => x.Email).Returns(email);
            memberMock.Setup(x => x.Username).Returns(userName);

            A.CallTo(() => _memberSignUpService.CreateMembersOnUserSave(userMock.Object)).Returns(memberMock.Object);
            MemberSignupService.Setup(x => x.CreateMembersOnUserSave(userMock.Object)).Returns(memberMock.Object);

            var newMock = _memberSignUpService.CreateMembersOnUserSave(userMock.Object);

            MemberViewModel memberViewModel = new MemberViewModel 
            { 
                Email = newMock.Email, 
                Name = newMock.Username, 
                UserName = newMock.Username 
            };

            Assert.AreEqual(userName, memberViewModel.UserName);
            Assert.AreEqual(email, memberViewModel.Email);
            Assert.AreEqual(userName, memberViewModel.Name);

            Assert.IsInstanceOf<IMember>(_memberSignUpService.CreateMembersOnUserSave(userMock.Object));
            MemberSignupService.Verify();
        }
    }
    [TestFixture]
    public class TestMediaComposerService : UmbracoBaseTest
    {
        [OneTimeSetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        [TestCase("", "")]
        [TestCase("TestValue", "TestValue2")]
        public void TestIsPropDirty(string value1, string value2)
        {
            var _mediaComposerService = A.Fake<IClientSaveComposerService>();
            var contentMock = new Mock<IContent>();
            var propertyCollectionMock = new Mock<PropertyCollection>();
            var propertyMock1 = new Mock<IPublishedProperty>();
            var idSetter = typeof(Property).GetMethod("GetValue", BindingFlags.Instance | BindingFlags.NonPublic);
            
            List<string> strings = new List<string>();
           

            propertyMock1.Setup(x => x.GetValue(null, null)).Returns(value1);
            propertyMock1.Setup(x => x.Alias).Returns(value1);
            var propertyMock2 = new Mock<IPublishedProperty>();
            propertyMock2.Setup(x => x.GetValue(null, null)).Returns(value2);
            propertyMock2.Setup(x => x.Alias).Returns(value2);

           
            List<IPublishedProperty> objList = new List<IPublishedProperty> {
            propertyMock1.Object,
            propertyMock2.Object
            };

            contentMock.Setup(x => x.Properties).Returns(value: null);
            if (!string.IsNullOrEmpty((string)propertyMock1.Object.GetValue()) && !string.IsNullOrEmpty((string)propertyMock2.Object.GetValue()))
            {
                contentMock.Setup(x => x.Properties).Returns(propertyCollectionMock.Object);
                
            }
                //propertyCollectionMock.Setup(x => x.ToList()).Returns(new List<Property> { new Mock<Property>().Object, new Mock<Property>().Object });

            MediaComposerService.Setup(x => x.LookForDirtyProperties<IContent>(contentMock.Object)).Returns(false);

            if (contentMock.Object.Properties != null)
                MediaComposerService.Setup(x => x.LookForDirtyProperties<IContent>(contentMock.Object)).Returns(true);

            if (!string.IsNullOrEmpty((string)propertyMock1.Object.GetValue()) && !string.IsNullOrEmpty((string)propertyMock2.Object.GetValue()))
            {
               foreach (var prop in objList) 
            {
                contentMock.Setup(x => x.GetValue(prop.Alias, null, null, false)).Returns(prop.GetValue(null, null));
                
                strings.Add(prop.Alias);
            }
            }
            contentMock.Setup(x => x.GetDirtyProperties()).Returns(strings);
            

            Assert.AreEqual(contentMock.Object.GetDirtyProperties().Any(), MediaComposerService.Object.LookForDirtyProperties(contentMock.Object));
        }
    }
    [TestFixture]
    public class UserServiceTest : UmbracoBaseTest
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }
        [Test]
        [TestCase("Mail@Mail.dk", "TestFirstName", "TestLastName")]
        public void Test_CreateUserFromJson(string email, string firstname, string lastname) 
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{{\"objects\":[{{\"EmailAddress\":{email}, \"Firstname\":{firstname}, \"Lastname\":{lastname}");
            MyUserService.Setup(x => x.CreateUser(stringBuilder.ToString()));
        }
    }

}