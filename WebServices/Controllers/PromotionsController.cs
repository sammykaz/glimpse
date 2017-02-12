using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServices.Models;
using WebServices.Helpers;
using System;

namespace WebServices.Controllers
{
    public class PromotionsController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        private readonly BlobHelper bh = new BlobHelper("glimpseimages", "XHIr8SaKFci88NT8Z+abpJaH1FeLC4Zq6ZRaIkaAJQc+N/1nwTqGPzDLdNZXGqcLNg+mK7ugGW3PyJsYU2gB7w==", "imagestorage");

        // GET: api/Promotions
        public IQueryable<Promotion> GetPromotions(bool active = false, string keyword = "")
        {
            IQueryable<Promotion> listOfPromos = db.Promotions;
            if (active)
            {
                listOfPromos = listOfPromos.Where(e => e.PromotionStartDate.CompareTo(DateTime.Now) <= 0 && e.PromotionEndDate.CompareTo(DateTime.Now) >= 0);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                listOfPromos = listOfPromos.Where(promo => promo.Title.Contains(keyword) || promo.Description.Contains(keyword));
            }

            return listOfPromos;
        }

        // GET: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult GetPromotion(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        [Route("api/Promotions/{id}/promotionclicks")]
        public IHttpActionResult GetVendorPromotions(int id)
        {
            List<PromotionClick> promotionClicksOfPromotion = db.PromotionClicks.Where(promoClick => promoClick.PromotionId == id).ToList();
            /*if (vendor == null)
            {
                return NotFound();
            } */

            return Ok(promotionClicksOfPromotion);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        [Route("api/Promotions/filter/{filterName}")]
        public IHttpActionResult GetVendorPromotions(Categories filterName)
        {
            List<Promotion> promotionsFiltered = db.Promotions.Where(promo => promo.Category == filterName).ToList();
            /*if (vendor == null)
            {
                return NotFound();
            } */

            return Ok(promotionsFiltered);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        //[Route("api/Promotions/Search/{filterName}")]
        public IHttpActionResult Search(string keyword)
        {
            List<Promotion> promotionsFiltered = db.Promotions.Where(promo => promo.Title.Contains(keyword) || promo.Description.Contains(keyword)).ToList();

            return Ok(promotionsFiltered);
        }


        // PUT: api/Promotions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotion(int id, Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bh.UploadFromByteArray(promotion.PromotionImage, promotion.PromotionImageURL);

            if (id != promotion.PromotionId)
            {
                return BadRequest();
            }

            db.Entry(promotion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
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

        // POST: api/Promotions
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult PostPromotion(Promotion promotion)
        {

            bh.UploadFromByteArray(promotion.PromotionImage, promotion.PromotionImageURL);

            if(promotion.RequestFromWeb == true)
            {
                int size = promotion.PromotionImages.Count;
                for (int i = 0; i < size; i++)
                {
                    string response = bh.UploadFromByteArray(promotion.PromotionImages.ElementAt(i).Image, promotion.PromotionImages.ElementAt(i).ImageURL);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Promotions.Add(promotion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotion.PromotionId }, promotion);
        }

        // DELETE: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult DeletePromotion(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            db.Promotions.Remove(promotion);
            db.SaveChanges();

            return Ok(promotion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionExists(int id)
        {
            return db.Promotions.Count(e => e.PromotionId == id) > 0;
        }
    }
}