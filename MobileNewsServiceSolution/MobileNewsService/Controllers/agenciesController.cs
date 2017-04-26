using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using MobileNewsServices.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsServices.Controllers
{
    public class agenciesController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();
        public agenciesController()
        {
            
        }

        public agenciesController(ITS_MobileNewsEntities context)
        {
            db = context;
        }

        // GET: api/agencies
        [ResponseType(typeof(IEnumerable<agencyViewModel>))]
        public IHttpActionResult Getagencies(int aid = 1, string rdate = null)
        {
            ApplicationAgencyFactory ApplicationAgencyFactory = new ApplicationAgencyFactory(db);
            var agencyList = ApplicationAgencyFactory.GetAllApplicationsAgencyIds(aid);
            AgencyFactory agencyFactory = new AgencyFactory(db);
            IEnumerable<agency> data = agencyFactory.Getagencies(agencyList, rdate);
            if (data == null) { return NotFound(); }
            List<agencyViewModel> listOfAgencies = new List<agencyViewModel>();

            foreach (var agency in data)
            {
                agencyViewModel Agency = new agencyViewModel();
                Agency.agency_id = agency.agency_id;
                Agency.agency_name = agency.agency_name;
                Agency.created_date = agency.created_date;
                Agency.modified_date = agency.modified_date;
                Agency.logical_delete_date = agency.logical_delete_date;
                listOfAgencies.Add(Agency);
            }
            IEnumerable<agencyViewModel> AgencyList = listOfAgencies;
            return Ok(AgencyList);
        }

        // GET: api/agencies/5
        [ResponseType(typeof(agency))]
        public async Task<IHttpActionResult> Getagency(int id)
        {
            agency agency = await db.agencies.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            return Ok(agency);
        }

        //// PUT: api/agencies/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> Putagency(int id, agency agency)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != agency.agency_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(agency).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!agencyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/agencies
        //[ResponseType(typeof(agency))]
        //public async Task<IHttpActionResult> Postagency(agency agency)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.agencies.Add(agency);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = agency.agency_id }, agency);
        //}

        //// DELETE: api/agencies/5
        //[ResponseType(typeof(agency))]
        //public async Task<IHttpActionResult> Deleteagency(int id)
        //{
        //    agency agency = await db.agencies.FindAsync(id);
        //    if (agency == null)
        //    {
        //        return NotFound();
        //    }

        //    db.agencies.Remove(agency);
        //    await db.SaveChangesAsync();

        //    return Ok(agency);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool agencyExists(int id)
        {
            ApplicationAgencyFactory ApplicationAgencyFactory = new ApplicationAgencyFactory(db);
            AgencyFactory agencyFactory = new AgencyFactory(db);
            agency a = agencyFactory.getAgency(id);
            bool returnValue = false;
            if (a!= null)
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}