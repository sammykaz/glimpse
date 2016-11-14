using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Glimpse.Core.Model;
using WebServices.Models;

namespace WebServices.Controllers.VendorsController
{
    public class VendorsController : ApiController
    {
        private readonly GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/Vendors
        public IQueryable<Vendor> GetVendors()
        {
            return db.Vendors;
        }

        // GET: api/Vendors/5
        [Route("api/Vendors/Search/{userName}/{password}")]
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendorByUserNamePassword(string userName, string password)
        {
            var vendor = db.Vendors.Where(e => e.UserName == userName && e.Password == password);
            if (vendor == null)
                return NotFound();

            return Ok(vendor);
        }
    

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendor(int id)
        {
            var vendor = db.Vendors.Find(id);
            if (vendor == null)
                return NotFound();

            return Ok(vendor);
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendor(int id, Vendor vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vendor.Id)
                return BadRequest();

            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                    return NotFound();
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vendors
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult PostVendor(Vendor vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Vendors.Add(vendor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = vendor.Id}, vendor);
        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult DeleteVendor(int id)
        {
            var vendor = db.Vendors.Find(id);
            if (vendor == null)
                return NotFound();

            db.Vendors.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool VendorExists(int id)
        {
            return db.Vendors.Count(e => e.Id == id) > 0;
        }
    }
}