using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IClientService
    {
        IContent CreateClient(string name);
    }
}