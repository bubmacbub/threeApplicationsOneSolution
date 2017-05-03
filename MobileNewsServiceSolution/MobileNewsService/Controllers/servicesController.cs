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
using MobileNewsModel.Entities;
using MobileNewsService.Models;
using MobileNewsBusinessLogic.Admin;

namespace MobileNewsService.Controllers
{
    public class servicesController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        public servicesController() { }

        public servicesController(ITS_MobileNewsEntities context)
        {
            db = context;
        }


        // GET: api/services?aid=1&lang=sp&rdate=2017-03-01
        [ResponseType(typeof(IEnumerable<serviceViewModel>))]
        public IHttpActionResult Getservices(int aid, string rdate = null, string lang = "en")
        {
            // aid now refers to APP_ID get list of agencies for app
            ApplicationAgencyFactory ApplicationAgencyFactory = new ApplicationAgencyFactory(db);
            List<int> agency_id_list = ApplicationAgencyFactory.GetAllApplicationsAgencyIds(aid);

            ServiceFactory serviceFactory = new ServiceFactory(db);

            IEnumerable<service> serviceQryResult = serviceFactory.FindservicesPerAgencyIdList(agency_id_list, rdate, lang);

            if (serviceQryResult == null) { return NotFound(); }
            List<serviceViewModel> listOfServices = new List<serviceViewModel>();

            foreach (var service in serviceQryResult)
            //foreach (var service in db.services)
            {
                serviceViewModel Service = new serviceViewModel();
                Service.service_id = service.service_id;
                Service.agency_id = service.agency_id;
                Service.language_id = service.language_id;
                Service.title = service.title;
                Service.service_content = service.service_content;
                Service.created_date = service.created_date;
                Service.modified_date = service.modified_date;
                Service.logical_delete_date = service.logical_delete_date;
                listOfServices.Add(Service);
            }
            IEnumerable<serviceViewModel> Services = listOfServices;
            //return serviceQryResult;
            return Ok(Services);
        }


        // GET: api/services/5
        [ResponseType(typeof(service))]
        public IHttpActionResult Details(int id)
        {
            service service = db.services.Where(c => c.service_id == id && c.logical_delete_date == null).FirstOrDefault();
            if (service == null){return NotFound();}
            
            serviceViewModel Service = new serviceViewModel();

            Service.service_id = service.service_id;
            Service.agency_id = service.agency_id;
            Service.language_id = service.language_id;
            Service.title = service.title;
            Service.service_content = service.service_content;
            Service.created_date = service.created_date;
            Service.modified_date = service.modified_date;
            Service.logical_delete_date = service.logical_delete_date;

            return Ok(Service);
        }

        // PUT: api/services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putservice(int id, service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.service_id)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!serviceExists(id))
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

        // POST: api/services
        [ResponseType(typeof(service))]
        public IHttpActionResult Postservice(service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.services.Add(service);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = service.service_id }, service);
        }

        // DELETE: api/services/5
        [ResponseType(typeof(service))]
        public IHttpActionResult Deleteservice(int id)
        {
            service service = db.services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            db.services.Remove(service);
            db.SaveChanges();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool serviceExists(int id)
        {
            return db.services.Count(e => e.service_id == id) > 0;
        }
    }
}