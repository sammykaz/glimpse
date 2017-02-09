using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServices.Models;
using WebServices.Helpers;

namespace WebServices.Controllers
{
    public class PromotionsController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/Promotions
        public IQueryable<Promotion> GetPromotions()
        {
            return db.Promotions;
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


        // PUT: api/Promotions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotion(int id, Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BlobHelper bh = new BlobHelper("storageglimpse", "UTaxV/U+abo8S1ORGCTyAVH4dUoFxl5jonIxMNAK/GUNP5u0IbNxa8WxyJpWbrg2aeUlm6S1NAkph/hW3i69wQ==", "imagestorage");
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
            BlobHelper bh = new BlobHelper("storageglimpse", "UTaxV/U+abo8S1ORGCTyAVH4dUoFxl5jonIxMNAK/GUNP5u0IbNxa8WxyJpWbrg2aeUlm6S1NAkph/hW3i69wQ==", "imagestorage");
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