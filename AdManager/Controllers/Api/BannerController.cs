using AdManager.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdManager.Controllers.Api
{

    public class BannerRequestParameters {
        public int ZoneID { get; set; }
    };

    public class BannerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Banner
        [ResponseType(typeof(BannerResponse))]
        public IHttpActionResult GetBanner([FromUri] BannerRequestParameters reqParams)
        {

            var zone = db.Zones.Find(reqParams.ZoneID);

            var query = (from p in db.Campaigns
                         where p.Budget > 0
                         where p.BannerImageWidth <= zone.AdPlacementWidth
                         where p.BannerImageHeight <= zone.AdPlacementHeight
                         orderby p.Revenue descending
                         select p).Take(1).ToList();
            Campaign campaign = query.Single();

            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var clickUrl = baseUrl + "/api/ClickTracker?CampaignId="+campaign.ID+"&ZoneId="+ reqParams.ZoneID;

            var imageHtml = "<img src='" + campaign.BannerImageUrl + "'/>";

            BannerResponse response = new BannerResponse
            {
                CPC = campaign.Revenue,
                Currency = campaign.Currency,
                BannerWidth = campaign.BannerImageWidth,
                BannerHeight = campaign.BannerImageHeight,
                BannerHtml = "<a href='"+ clickUrl+"'>"+ imageHtml + "</a>"
            };
            return Ok(response);
        }

    }
}
