using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Links;
using events.tac.local.Models;
using Sitecore.Globalization;


namespace events.tac.local.Controllers
{
    public class OverviewController : Controller
    {
        // GET: Overview
        public ActionResult Index()
        {
            var model = new OverviewList()
            {
                ReadMore = Translate.Text("Read More")
            };

            model.AddRange(RenderingContext.Current.Rendering.Item.GetChildren(Sitecore.Collections.ChildListOptions.SkipSorting).OrderByDescending(i => i.Statistics.Created).Select(i => new OverviewItem()
            {
                title = new HtmlString(FieldRenderer.Render(i, "ContentHeading")),
                image = new HtmlString(FieldRenderer.Render(i, "DecorationBanner", "mw=500&mh=333")),
                URL = LinkManager.GetItemUrl(i)
            }
             ));
            return View(model);
        }

    }
}