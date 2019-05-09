using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;
using TAC.Utils.Helpers;
using Sitecore.Data;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics.Outcome.Model;




namespace events.tac.local.Controllers
{
    public class SubscriberFormController : Controller
    {
        // GET: SubscriberForm
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(string email)
        {
            Tracker.Current.Session.Identify(email);
            var contact = Tracker.Current.Contact;
            var emails = contact.GetFacet<IContactEmailAddresses>("Emails");
            if (!emails.Entries.Contains("personal"))
            {
                emails.Preferred = "personal";
                var personalEmail = emails.Entries.Create("personal");
                personalEmail.SmtpAddress = email;
            }
            var nid = ID.NewID;

            var oc = new ContactOutcome(
                nid, 
                new ID("{AB38C0FE-C9B6-4974-836B-5A74840BCAC7}"), 
                new ID(contact.ContactId));
            Tracker.Current.RegisterContactOutcome(oc);

            return View("Confirmation");
        }

    }
}