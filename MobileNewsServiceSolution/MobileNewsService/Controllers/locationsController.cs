using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using MobileNewsService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsService.Controllers
{
    public class locationsController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        public locationsController()
        {
        }

        public locationsController(ITS_MobileNewsEntities context)
        {
            db = context;
        }
        // GET: api/locations
        [ResponseType(typeof(IEnumerable<locationViewModel>))]
        public IHttpActionResult Getlocations(int aid, string lang = "en", string rdate = null)
        {
            // aid now refers to APP_ID get list of agencies for app

            ApplicationAgencyFactory ApplicationAgencyFactory = new ApplicationAgencyFactory(db);
            List<int> agency_id_list = ApplicationAgencyFactory.GetAllApplicationsAgencyIds(aid);

            LocationFactory locationfactory = new LocationFactory(db);
            IEnumerable<location> locationQryResult = locationfactory.FindLocationsByAgencyList(agency_id_list, rdate);

            if (rdate == null)
            {
                locationQryResult = db.locations.Where(c => agency_id_list.Contains(c.agency_id ?? 0) && c.language.language_short == lang);
            }
            else
            {
                DateTime releaseDate = DateTime.Parse(rdate);
                locationQryResult = db.locations.Where(c => agency_id_list.Contains(c.agency_id ?? 0) && c.language.language_short == lang && c.modified_date >= releaseDate);
            }
            if (locationQryResult == null) { return NotFound(); }
            List<locationViewModel> listofLocations = new List<locationViewModel>();

            foreach (var location in locationQryResult)
            {
                locationViewModel Location = new locationViewModel();
                Location.location_id = location.location_id;
                Location.agency_id = location.agency_id;
                Location.location_name = location.location_name;
                Location.address = location.address;
                Location.city = location.city;
                Location.state = location.state;
                Location.zip = location.zip;
                Location.latitude = location.latitude;
                Location.longitude = location.longitude;
                Location.website = location.website;
                Location.phone = location.phone;
                Location.email = location.email;
                Location.comment = location.comment;
                Location.language_id = location.language_id;
                listofLocations.Add(Location);
            }
            IEnumerable<locationViewModel> Locations = listofLocations;
            return Ok(Locations);
        }

        // GET: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocation(int id)
        {
            location location = db.locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation(int id, location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.location_id)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/locations
        [ResponseType(typeof(location))]
        public IHttpActionResult Postlocation(location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.locations.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.location_id }, location);
        }

        // DELETE: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Deletelocation(int id)
        {
            location location = db.locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool locationExists(int id)
        {
            return db.locations.Count(e => e.location_id == id) > 0;
        }
    }
}