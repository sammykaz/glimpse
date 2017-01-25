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
using WebServices.Helpers;
using WebServices.Models;

namespace WebServices.Controllers
{
    public class PromotionImagesController : ApiController
    {
        private GlimpseDbContext db = new GlimpseDbContext();

        // GET: api/PromotionImages
        public IQueryable<PromotionImage> GetPromotionImages()
        {
            return db.PromotionImages;
        }

        // GET: api/PromotionImages/5
        [ResponseType(typeof(PromotionImage))]
        public IHttpActionResult GetPromotionImage(int id)
        {
            PromotionImage promotionImage = db.PromotionImages.Find(id);
            if (promotionImage == null)
            {
                return NotFound();
            }

            return Ok(promotionImage);
        }

        // PUT: api/PromotionImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotionImage(int id, PromotionImage promotionImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotionImage.PromotionImageId)
            {
                return BadRequest();
            }

            db.Entry(promotionImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionImageExists(id))
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

        // POST: api/PromotionImages
        [ResponseType(typeof(PromotionImage))]
        public IHttpActionResult PostPromotionImage(PromotionImage promotionImage)
        {
            BlobHelper bh = new BlobHelper("storageglimpse", "UTaxV/U+abo8S1ORGCTyAVH4dUoFxl5jonIxMNAK/GUNP5u0IbNxa8WxyJpWbrg2aeUlm6S1NAkph/hW3i69wQ==", "imagestorage");
            bh.UploadFromByteArray(promotionImage.Image, promotionImage.ImageURL);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PromotionImages.Add(promotionImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotionImage.PromotionImageId }, promotionImage);
        }

        // DELETE: api/PromotionImages/5
        [ResponseType(typeof(PromotionImage))]
        public IHttpActionResult DeletePromotionImage(int id)
        {
            PromotionImage promotionImage = db.PromotionImages.Find(id);
            if (promotionImage == null)
            {
                return NotFound();
            }

            db.PromotionImages.Remove(promotionImage);
            db.SaveChanges();

            return Ok(promotionImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionImageExists(int id)
        {
            return db.PromotionImages.Count(e => e.PromotionImageId == id) > 0;
        }
    }
}