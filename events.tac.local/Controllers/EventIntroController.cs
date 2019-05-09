using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;


namespace events.tac.local.Controllers
{
    public class EventIntroController : Controller
    {
        // GET: EventIntro
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        public static EventIntro CreateModel()
        {
            Item curItem = RenderingContext.Current.ContextItem;
            var eventIntro = new EventIntro();
            eventIntro.Heading = new HtmlString(FieldRenderer.Render(curItem, "ContentHeading"));
            eventIntro.Intro = new HtmlString(FieldRenderer.Render(curItem, "ContentIntro"));
            eventIntro.Body = new HtmlString(FieldRenderer.Render(curItem, "ContentBody"));
            eventIntro.Highlights = new HtmlString(FieldRenderer.Render(curItem, "Highlights"));
            eventIntro.EventImage = new HtmlString(FieldRenderer.Render(curItem, "Event Image", "mw=400"));
            eventIntro.StartDate = new HtmlString(FieldRenderer.Render(curItem, "Start Date"));
            eventIntro.Duration = new HtmlString(FieldRenderer.Render(curItem, "Duration"));
            eventIntro.Difficulty = new HtmlString(FieldRenderer.Render(curItem, "Difficulty Level"));
            return eventIntro;
        }

    }
}