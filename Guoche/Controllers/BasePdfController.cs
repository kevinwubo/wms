using ReportManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class BasePdfController : Controller
    {
        private readonly HtmlViewRenderer htmlViewRenderer;

        public BasePdfController()
        {
            this.htmlViewRenderer = new HtmlViewRenderer();
        }

        protected string ViewPdf(string viewName,object model)
        {
            // Render the view html to a string.
            string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName,model);
            return htmlText;
        }

    }
}
