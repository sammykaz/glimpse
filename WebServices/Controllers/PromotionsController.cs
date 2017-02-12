using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServices.Models;
using WebServices.Helpers;
using Serilog;

namespace WebServices.Controllers
{
    public class PromotionsController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/Promotions
        public IQueryable<Promotion> GetPromotions()
        {
            Log.Information("Getting all promotions");
            return db.Promotions;
        }

        // GET: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult GetPromotion(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                Log.Error("Could not find promotion with id: {@id}", id);
                return NotFound();
            }

            Log.Information("Found promotion with id: {@id}", id);
            return Ok(promotion);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        [Route("api/Promotions/{id}/promotionclicks")]
        public IHttpActionResult GetVendorPromotions(int id)
        {
            Log.Information("Attemping to get vendor promotion(s) by id: {@id}", id);
            List<PromotionClick> promotionClicksOfPromotion = db.PromotionClicks.Where(promoClick => promoClick.PromotionId == id).ToList();
            /*if (vendor == null)
            {
                return NotFound();
            } */

            Log.Information("Returning vendor promotions that was found by id: {@id}", id);
            return Ok(promotionClicksOfPromotion);
        }

        // GET: api/Vendors/5/promotions
        [ResponseType(typeof(Vendor))]
        [Route("api/Promotions/filter/{filterName}")]
        public IHttpActionResult GetVendorPromotions(Categories filterName)
        {
            Log.Information("Attemping to get vendor promotion(s) by category: {@filterName}", filterName);
            List<Promotion> promotionsFiltered = db.Promotions.Where(promo => promo.Category == filterName).ToList();
            /*if (vendor == null)
            {
                return NotFound();
            } */

            Log.Information("Returning vendor promotions that was found by cateogory: {@filterName}", filterName);
            return Ok(promotionsFiltered);
        }


        // PUT: api/Promotions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotion(int id, Promotion promotion)
        {
            Log.Information("Attempting to update promotion for promotion: {@promotion} with id {@id}", promotion.Title, id);
            if (!ModelState.IsValid)
            {
                Log.Error("Invalid model state for promotion: {@promotion} with id: {@id}", promotion.Title, id);
                return BadRequest(ModelState);
            }

            if (id != promotion.PromotionId)
            {
                Log.Error("Id: {@id} is the incorrect id for promotion {@promotion}", id, promotion.Title);
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
                    Log.Error("Promotion with id: {@id} does not exist!", id);
                    return NotFound();
                }
                else
                {
                    Log.Error("Update Operation has failed for promtion with id: {@id}", id);
                    throw;
                }
            }
            Log.Information("Promotion with id: {@id} has been updated!", id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Promotions
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult PostPromotion(Promotion promotion)
        {
            Log.Information("Attempting to add promotion: {@promotion}", promotion.Title);
            BlobHelper bh = new BlobHelper("storageglimpse", "UTaxV/U+abo8S1ORGCTyAVH4dUoFxl5jonIxMNAK/GUNP5u0IbNxa8WxyJpWbrg2aeUlm6S1NAkph/hW3i69wQ==", "imagestorage");
            bh.UploadFromByteArray(promotion.PromotionImage, promotion.PromotionImageURL);


            if (!ModelState.IsValid)
            {
                Log.Error("Invalid model state for promotion: {@promotion}", promotion.Title);
                return BadRequest(ModelState);
            }

            db.Promotions.Add(promotion);
            db.SaveChanges();

            Log.Information("Promotion: {@promotion} has been added to the database!", promotion.Title);
            return CreatedAtRoute("DefaultApi", new { id = promotion.PromotionId }, promotion);
        }

        // DELETE: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult DeletePromotion(int id)
        {
            Log.Information("Attemping to delete promotion with id: {@id}", id);
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                Log.Error("Promotion with id: {@id} does not exist!", id);
                return NotFound();
            }

            db.Promotions.Remove(promotion);
            db.SaveChanges();

            Log.Information("Promotion with id: {@id} has been deleted.", id);
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