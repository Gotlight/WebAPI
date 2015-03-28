using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MenuCatalogController : ApiController
    {
        private RESTAURANTEntities2 db = new RESTAURANTEntities2();

        // GET api/MenuCatalog
        public IQueryable<MenuCatalog> GetMenuCatalogs()
        {
            return db.MenuCatalogs;
        }

        // GET api/MenuCatalog/5
        [ResponseType(typeof(MenuCatalog))]
        public async Task<IHttpActionResult> GetMenuCatalog(Guid id)
        {
            MenuCatalog menucatalog = await db.MenuCatalogs.FindAsync(id);
            if (menucatalog == null)
            {
                return NotFound();
            }

            return Ok(menucatalog);
        }

        // PUT api/MenuCatalog/5
        public async Task<IHttpActionResult> PutMenuCatalog(Guid id, MenuCatalog menucatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menucatalog.ID)
            {
                return BadRequest();
            }

            db.Entry(menucatalog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCatalogExists(id))
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

        // POST api/MenuCatalog
        [ResponseType(typeof(MenuCatalog))]
        public async Task<IHttpActionResult> PostMenuCatalog(MenuCatalog menucatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuCatalogs.Add(menucatalog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuCatalogExists(menucatalog.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = menucatalog.ID }, menucatalog);
        }

        // DELETE api/MenuCatalog/5
        [ResponseType(typeof(MenuCatalog))]
        public async Task<IHttpActionResult> DeleteMenuCatalog(Guid id)
        {
            MenuCatalog menucatalog = await db.MenuCatalogs.FindAsync(id);
            if (menucatalog == null)
            {
                return NotFound();
            }

            db.MenuCatalogs.Remove(menucatalog);
            await db.SaveChangesAsync();

            return Ok(menucatalog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuCatalogExists(Guid id)
        {
            return db.MenuCatalogs.Count(e => e.ID == id) > 0;
        }
    }
}