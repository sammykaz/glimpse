﻿using System;
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


        // GET: api/Vendors/Search/lala@gmail.com/
        //trailing slash is important or else 404 error
        [Route("api/Vendors/Search/{email}/")]
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendor(string email)
        {
            //for most email providers, upper case is the same as lower
            Vendor vendor = db.Vendors.FirstOrDefault(e => e.Email.ToLower().Equals(email.ToLower()));
            if (vendor == null)
            {
                return Ok();
            }

            return Ok(vendor);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        [Route("api/Vendors/{id}/promotions")]
        public IHttpActionResult GetVendorPromotions(int id)
        {
            List<Promotion> promosOfVendor = db.Promotions.Where(promo => promo.VendorId == id).ToList();
            /*if (vendor == null)
            {
                return NotFound();
            } */

            return Ok(promosOfVendor);
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

        /*
        // PUT: api/Vendors/5/promotions
        [ResponseType(typeof(void))]
        [Route("api/Vendors/{id}/promotions")]
        public IHttpActionResult PutCollectionVendor(int id, Vendor vendor)
        {
            if (vendor.Promotions.Count == 0)
            {
                vendor.CompanyName = "its empty";
            }
            else
            {
                vendor.CompanyName = vendor.Promotions.ElementAt(0).Description;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            db.Entry(vendor.Promotions).State = EntityState.Modified;
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
        */

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