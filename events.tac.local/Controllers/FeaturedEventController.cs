using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Links;


namespace events.tac.local.Controllers
{
    public class FeaturedEventController : Controller
    {
        // GET: FeaturedEvent
        public ActionResult Index()
        {
            return View(CreatModel());
        }
        public static FeaturedEvent CreatModel()
        {
            Item curItem = RenderingContext.Current.Rendering.Item;
            var featuredEvent = new FeaturedEvent()
            {
                Heading = new HtmlString(FieldRenderer.Render(curItem, "ContentHeading")),
                Intro = new HtmlString(FieldRenderer.Render(curItem, "ContentIntro")),
                EventImage = new HtmlString(FieldRenderer.Render(curItem, "Event Image", "mw=500"))
            };


            var cssClass = RenderingContext.Current.Rendering.Parameters["CssClass"];


            if (!string.IsNullOrEmpty(cssClass))
            {
                var refItem = Sitecore.Context.Database.GetItem(cssClass);
                if (refItem != null)
                {
                    featuredEvent.CssClass = refItem["Class"];
                }
            }
            //featuredEvent.CssClass = "bg-primary";

            featuredEvent.URL = LinkManager.GetItemUrl(curItem);

            return featuredEvent;
        }

    }
}