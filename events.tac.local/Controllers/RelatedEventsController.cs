using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using events.tac.local.Models;


namespace events.tac.local.Controllers
{
    public class RelatedEventsController : Controller
    {
        // GET: RelatedEvents
        public ActionResult Index()
        {
            Item curentItem = RenderingContext.Current.Rendering.Item;
            if (curentItem == null) return new EmptyResult();
            MultilistField refs = RenderingContext.Current.ContextItem.Fields["Related Events"];
            if (refs == null) return new EmptyResult();
            var RelatedEvents = refs.GetItems().Select(i => new NavigationItem
            {
                title = new HtmlString(i.DisplayName),
                URL = LinkManager.GetItemUrl(i)
            }
            );

            return View(RelatedEvents);
        }

    }
}