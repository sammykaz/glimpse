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
    public class PromotionClicksController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/PromotionClicks
        public IQueryable<PromotionClick> GetPromotionClicks()
        {
            return db.PromotionClicks;
        }

        // GET: api/PromotionClicks/5
        [ResponseType(typeof(PromotionClick))]
        public IHttpActionResult GetPromotionClick(int id)
        {
            PromotionClick promotionClick = db.PromotionClicks.Find(id);
            if (promotionClick == null)
            {
                return NotFound();
            }

            return Ok(promotionClick);
        }

        // PUT: api/PromotionClicks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotionClick(int id, PromotionClick promotionClick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotionClick.PromotionClickId)
            {
                return BadRequest();
            }

            db.Entry(promotionClick).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionClickExists(id))
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

        // POST: api/PromotionClicks
        [ResponseType(typeof(PromotionClick))]
        public IHttpActionResult PostPromotionClick(PromotionClick promotionClick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PromotionClicks.Add(promotionClick);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotionClick.PromotionClickId }, promotionClick);
        }

        // DELETE: api/PromotionClicks/5
        [ResponseType(typeof(PromotionClick))]
        public IHttpActionResult DeletePromotionClick(int id)
        {
            PromotionClick promotionClick = db.PromotionClicks.Find(id);
            if (promotionClick == null)
            {
                return NotFound();
            }

            db.PromotionClicks.Remove(promotionClick);
            db.SaveChanges();

            return Ok(promotionClick);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionClickExists(int id)
        {
            return db.PromotionClicks.Count(e => e.PromotionClickId == id) > 0;
        }
    }
}