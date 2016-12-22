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
using WebServices.Models;

namespace WebServices.Controllers
{
    public class VendorsController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/Vendors
        public IQueryable<Vendor> GetVendors()
        {
            return db.Vendors;
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendor(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // GET: api/Vendors/Search/example@gmail.com
        [Route("api/Vendors/Search/{email}")]
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendorByEmail(string email)
        {
            try
            {
                var vendor = db.Vendors.Where(e => e.Email == email);
                if (vendor == null)
                    return NotFound();

                return Ok(vendor);
            }
            catch (WebException webExcp)
            {
                // If you reach this point, an exception has been caught.
                Console.WriteLine("1. A WebException has been caught.");
                // Write out the WebException message.
                Console.WriteLine(webExcp.ToString());
                // Get the WebException status code.
                WebExceptionStatus status = webExcp.Status;
                // If status is WebExceptionStatus.ProtocolError, 
                //   there has been a protocol error and a WebResponse 
                //   should exist. Display the protocol error.
                if (status == WebExceptionStatus.ProtocolError)
                {
                    Console.Write("The server returned protocol error ");
                    // Get HttpWebResponse so that you can check the HTTP status code.
                    HttpWebResponse httpResponse = (HttpWebResponse)webExcp.Response;
                    Console.WriteLine((int)httpResponse.StatusCode + " - "
                       + httpResponse.StatusCode);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("2. " + e.ToString());
                return null;
            }
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendor(int id, Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult PostVendor(Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendors.Add(vendor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendor.VendorId }, vendor);
        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult DeleteVendor(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            db.Vendors.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorExists(int id)
        {
            return db.Vendors.Count(e => e.VendorId == id) > 0;
        }
    }
}