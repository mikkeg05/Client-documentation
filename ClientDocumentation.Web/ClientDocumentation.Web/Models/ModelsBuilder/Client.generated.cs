//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.18.5
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace ClientDocumentation.Web.Models.ModelsBuilder
{
	/// <summary>Client</summary>
	[PublishedModel("client")]
	public partial class Client : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		public new const string ModelTypeAlias = "client";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Client, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public Client(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Branching
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("branching")]
		public virtual global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Branch> Branching => this.Value<global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Branch>>("branching");

		///<summary>
		/// Client data
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("clientData")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement> ClientData => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement>>("clientData");

		///<summary>
		/// ClientLogo
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("clientLogo")]
		public virtual global::Umbraco.Core.Models.MediaWithCrops ClientLogo => this.Value<global::Umbraco.Core.Models.MediaWithCrops>("clientLogo");

		///<summary>
		/// Daily development: What to expect as developer
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("dailyDevelopment")]
		public virtual global::System.Web.IHtmlString DailyDevelopment => this.Value<global::System.Web.IHtmlString>("dailyDevelopment");

		///<summary>
		/// Deployment description: Descripe how deployment is handled or link to guide
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("deploymentDescription")]
		public virtual global::System.Web.IHtmlString DeploymentDescription => this.Value<global::System.Web.IHtmlString>("deploymentDescription");

		///<summary>
		/// Deployment Steps
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("deploymentSteps")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement> DeploymentSteps => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement>>("deploymentSteps");

		///<summary>
		/// Deployment Strategy
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("deploymentStrategy")]
		public virtual global::System.Web.IHtmlString DeploymentStrategy => this.Value<global::System.Web.IHtmlString>("deploymentStrategy");

		///<summary>
		/// Description: A short summery of, how PM is enforced on this client
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("description")]
		public virtual global::System.Web.IHtmlString Description => this.Value<global::System.Web.IHtmlString>("description");

		///<summary>
		/// Environments
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("environments")]
		public virtual global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Environment> Environments => this.Value<global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Environment>>("environments");

		///<summary>
		/// Features
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("features")]
		public virtual global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.KeyFeature> Features => this.Value<global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.KeyFeature>>("features");

		///<summary>
		/// Integrations
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("integrations")]
		public virtual global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Integration> Integrations => this.Value<global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.Integration>>("integrations");

		///<summary>
		/// IsHidden
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("isHidden")]
		public virtual bool IsHidden => this.Value<bool>("isHidden");

		///<summary>
		/// Items
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("items")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement> Items => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedElement>>("items");

		///<summary>
		/// Major Tech stack
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("majorTechStack")]
		public virtual global::System.Collections.Generic.IEnumerable<string> MajorTechStack => this.Value<global::System.Collections.Generic.IEnumerable<string>>("majorTechStack");

		///<summary>
		/// MutlipleImagesTest
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("mutlipleImagesTest")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.MediaWithCrops> MutlipleImagesTest => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.MediaWithCrops>>("mutlipleImagesTest");

		///<summary>
		/// Need to know features: Inform about the key features, that are used everyday in development and testing
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("needToKnowFeatures")]
		public virtual string NeedToKnowFeatures => this.Value<string>("needToKnowFeatures");

		///<summary>
		/// Prerequisite: Frameworks needed etc..
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("prerequisite")]
		public virtual global::System.Web.IHtmlString Prerequisite => this.Value<global::System.Web.IHtmlString>("prerequisite");

		///<summary>
		/// Primary Systems
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("primarySystems")]
		public virtual global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.SystemType> PrimarySystems => this.Value<global::System.Collections.Generic.IEnumerable<global::ClientDocumentation.Web.Models.ModelsBuilder.SystemType>>("primarySystems");

		///<summary>
		/// testVideo
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("testVideo")]
		public virtual global::Umbraco.Core.Models.MediaWithCrops TestVideo => this.Value<global::Umbraco.Core.Models.MediaWithCrops>("testVideo");

		///<summary>
		/// Version Control
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.5")]
		[ImplementPropertyType("versionControlValue")]
		public virtual string VersionControlValue => this.Value<string>("versionControlValue");
	}
}
