//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using NUnit;
//using NPoco;
//using Umbraco.Tests.TestHelpers;
//using Umbraco.Core.Services;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Umbraco.Tests.Testing;
//using ClientDocumentation.Web.Business.Interfaces;
//using System.Text;
//using NUnit.Framework;
//using Umbraco.Core.Models.Membership;
//using Assert = NUnit.Framework.Assert;
//using Moq;
//using Umbraco.Tests.Integration;
//using Umbraco.Tests.Services;
//using Umbraco.Tests.Persistence.Repositories;
//using Umbraco.Core.Services.Implement;
//using Umbraco.Core;
//using ClientDocumentation.Web.Business.Services;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.DependencyInjection;

//namespace ClientDocumentation.Web.Test
//{
//    [TestFixture]
//    [UmbracoTest(Database = UmbracoTestOptions.Database.NewSchemaPerTest)]
//    public class TestClassReworked : Umbraco.Tests.Testing.UmbracoTestBase
//    {

//        private readonly Mock<IContentTypeService> _contentTypeService = new Mock<IContentTypeService>();
//        private readonly Mock<IContentService> _contentService = new Mock<IContentService>();
//        private readonly Mock<IDataTypeService> _dataTypeService = new Mock<IDataTypeService>();
//        private readonly Mock<IEntityService> _entityService = new Mock<IEntityService>();
//        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
//        private readonly Mock<IMyUserService> _myUserService = new Mock<IMyUserService>();
        

//        [SetUp]
//        public override void SetUp()
//        {
//            TestOptionAttributeBase.ScanAssemblies.Add((GetType().Assembly));
//            base.SetUp();
//        }


//        //[TestMethod]
//        //[TestCase("Mail@Mail.dk", "TestFirstName", "TestLastName")]
//        //[TestCase("MailMail.dk", "", "")]
//        //public void TestMethod1(string email, string firstname, string lastname)
//        //{
//        //    var ser = base.Factory.GetInstance(typeof(MyUserService));
//        //    var user = new Mock<IUser>();
//        //    user.SetupProperty(n => n.Username);
//        //    user.SetupProperty(e => e.Email);
//        //    user.Object.Username = firstname + lastname;
//        //    user.Object.Email = email;

//        //    StringBuilder stringBuilder = new StringBuilder();
//        //    stringBuilder.Append($"{{\"objects\":[{{\"EmailAddress\":{email}, \"Firstname\":{firstname}, \"Lastname\":{lastname}");
            


//        //    _myUserService.Setup(x => x.CreateUser(stringBuilder.ToString())).Returns(new List<IUser> { 
//        //        new Mock<IUser>()
//        //        .SetupProperty(n => n.Username, firstname + lastname)
//        //        .SetupProperty(e => e.Email, email).Object 
//        //    });

//        //    Assert.AreEqual(user.Object.Username, _myUserService.Object.CreateUser(stringBuilder.ToString()).FirstOrDefault().Username);

//        //}


//    }
//}