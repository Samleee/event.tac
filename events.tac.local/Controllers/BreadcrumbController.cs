using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using events.tac.local.Models;


namespace events.tac.local.Controllers
{
    public class BreadcrumbController : Controller
    {
        // GET: Breadcrumb
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        public static IEnumerable<NavigationItem> CreateModel()
        {
            IEnumerable<NavigationItem> NavList = new List<NavigationItem>();

            Item curItem = RenderingContext.Current.PageContext.Item;
            //Item homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            //var startItemStr = RenderingContext.Current.Rendering.DataSource;
            //Item startItem = Sitecore.Context.Database.GetItem(startItemStr);
            Item startItem = RenderingContext.Current.Rendering.Item;
            var NavItems = curItem.Axes.GetAncestors().Where(i => i.Axes.IsDescendantOf(startItem)).Concat(new Item[] { curItem }).ToList();

            NavList = NavItems.Select(s => new NavigationItem
            {
                //title = s.DisplayName
                title = new HtmlString(FieldRenderer.Render(s, "ContentHeading", "DisableWebEditing=true")),
                URL = LinkManager.GetItemUrl(s),
                Active = (s.ID == curItem.ID)
            });



            return NavList;

        }

    }
}