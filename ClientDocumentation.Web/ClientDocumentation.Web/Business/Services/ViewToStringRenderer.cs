using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientDocumentation.Web.Business.Services
{
    public static class ViewToStringRenderer
    {
        public static string RenderViewToString<TModel>(ControllerContext controllerContext, string viewName, TModel model) 
        {
            ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
            if (viewEngineResult.View == null) 
            {
                return null;
            }
            IView view = viewEngineResult.View;
            using (var stringWriter = new StringWriter()) {
                var viewContext = new ViewContext(controllerContext, view, new ViewDataDictionary<TModel>(model), new TempDataDictionary(), stringWriter);
                view.Render(viewContext, stringWriter);
                return stringWriter.ToString();
            }
        }
    }
}