using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Models
{
    public class EventDetails : SearchResultItem
    {
        public string ContentHeading { get; set; }
        public string ContentIntro { get; set; }
        public DateTime EventStartDate { get; set; }
        public HtmlString EventImage
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(GetItem(), "Event Image", "DisableWebEditing=true&mw=150"));
            }
        }

    }
}
