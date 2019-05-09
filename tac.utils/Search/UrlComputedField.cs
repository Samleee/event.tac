using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
//using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Configuration;
using Sitecore.Resources.Media;
using System;

namespace TAC.Utils.Search
{
    class UrlComputedField : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            //throw new NotImplementedException();
            Item item = (Item)(indexable as SitecoreIndexableItem);
            if (item == null) return null;
            if (item.Paths.IsMediaItem)
            {
                return MediaManager.GetMediaUrl(item);
            }
            UrlOptions defaultUrlOptions = LinkManager.GetDefaultUrlOptions();
            //defaultUrlOptions.LanguageEmbedding= LanguageEmbedding.Never;
            defaultUrlOptions.SiteResolving = Settings.Rendering.SiteResolving;

            var link = LinkManager.GetItemUrl(item, defaultUrlOptions);
            var noProtocol = link.Replace("://", String.Empty);
            var siteName = noProtocol.Split('/');

            var processedLink = noProtocol.Replace(siteName[0], String.Empty);
            return processedLink;



        }
    }
}
