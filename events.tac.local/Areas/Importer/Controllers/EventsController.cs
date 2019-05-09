using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Data;
using Sitecore.SecurityModel;


namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;
            string message = null;
            using (var reader = new System.IO.StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch (Exception ex)
                {

                }
            }

            var database = Sitecore.Configuration.Factory.GetDatabase("master");
            var parentItem = database.GetItem(parentPath);
            var templateID = new TemplateID(new ID("{9050E8E3-CFF0-47A6-AB29-C0FBB75AA0AF}"));
            using (new SecurityDisabler())
            {
                foreach (var ev in events)
                {
                    var name = ItemUtil.ProposeValidItemName(ev.ContentHeading);
                    Item item = parentItem.Add(name, templateID);
                    item.Editing.BeginEdit();
                    item["ContentHeading"] = ev.ContentHeading;
                    item["contentIntro"] = ev.ContentIntro;
                    item["Highlights"] = ev.Highlights;
                    item["Start Date"] = Sitecore.DateUtil.ToIsoDate(ev.StartDate);
                    item["Duration"] = ev.Duration.ToString();
                    item["Difficulty Level"] = ev.Difficulty.ToString();
                    item[Sitecore.FieldIDs.Workflow] = "{6DC87E75-2E08-4869-A3DE-3C546C42B1C8}";
                    item[Sitecore.FieldIDs.WorkflowState] = "{2CF0D339-5B12-4A7F-B450-AF8542639C94}";
                    item.Editing.EndEdit();
                }
            }
            return View();
        }
    }
}
