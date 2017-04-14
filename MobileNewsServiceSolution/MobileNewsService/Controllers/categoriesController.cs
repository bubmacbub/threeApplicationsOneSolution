using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using NYSOFA_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsServices.Controllers
{

    public class categoriesController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/categories/2?aid=1
        // we are getting categories by category_type and agency id

        public IEnumerable<categoryViewModel> Getcategories(int aid, string rdate = null)
        {
            
            IEnumerable<category> data= new CategoryFactory(db).GetApplicationsCategories(aid, rdate);
            
            List<categoryViewModel> Categories = new List<categoryViewModel>();
            
            foreach (var category in data)
            {
                categoryViewModel Category = new categoryViewModel();
                Category.category_id = category.category_id;
                Category.agency_id = category.agency_id;
                Category.category_name = category.category_name;
                Category.created_date = category.created_date;
                Category.modified_date = category.modified_date;
                Category.logical_delete_date = category.logical_delete_date;
                Categories.Add(Category);
            }
            return Categories;
        }

        //// GET: api/categories/5
        //// unlikely to use this, an individual category is not that useful by itself
        //[ResponseType(typeof(category))]
        //public async Task<IHttpActionResult> Getcategory(int id)
        //{
        //    category category = await db.categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category);
        //}

        //// PUT: api/categories/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> Putcategory(int id, category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != category.category_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!categoryExists(id))
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

        //// POST: api/categories
        //[ResponseType(typeof(category))]
        //public async Task<IHttpActionResult> Postcategory(category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.categories.Add(category);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = category.category_id }, category);
        //}

        //// DELETE: api/categories/5
        //[ResponseType(typeof(category))]
        //public async Task<IHttpActionResult> Deletecategory(int id)
        //{
        //    category category = await db.categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    db.categories.Remove(category);
        //    await db.SaveChangesAsync();

        //    return Ok(category);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool categoryExists(int id)
        {
            bool returnValue = false;
            category c = new CategoryFactory(db).Find(id);
            if(c != null)
            {
                returnValue = true;
            }
            return returnValue;
        }
    }
}