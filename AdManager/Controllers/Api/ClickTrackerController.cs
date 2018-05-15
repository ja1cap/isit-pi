using AdManager.Models;
using System.Web.Http;

namespace AdManager.Controllers.Api
{

    public class ClickTrackerRequestParameters
    {
        public int ZoneID { get; set; }
        public int CampaignID { get; set; }
    }

    public class ClickTrackerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ClickTracker
        public IHttpActionResult GetClickTracker([FromUri] ClickTrackerRequestParameters reqParams)
        {
            var redirecUrl = "https://google.com";
            var campaign = db.Campaigns.Find(reqParams.CampaignID);
            if (campaign != null && campaign.Budget > 0)
            {
                campaign.Budget = campaign.Budget - campaign.Revenue;
                db.SaveChanges();
                redirecUrl = campaign.ClickUrl;
            }
            return Redirect(redirecUrl);
        }

    }
}
