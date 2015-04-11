using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class OrganizationController : ApiController
    {
        private RESTAURANTEntities2 db = new RESTAURANTEntities2();

        // GET api/Organization
        public IQueryable GetOrganizations()
        {
            var organizations = db.Organizations.Select(x => new {x.ID, x.OrganizationName, x.OrganizationType.OrganizationTypeName, x.OrganizationLogo } );
            return organizations;
        }
        // GET api/Organization/5
       // [ResponseType(typeof(Organization))]
        public async Task<IHttpActionResult> GetOrganization(Guid id)
        {
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
               return Ok(organization);

        }

        [Route("api/organization/{OrganizationId}/{LanguageCode}/MenuCatalog")]
        public IQueryable GetMenuCatalogByOrganization(Guid OrganizationId, string LanguageCode)
        {
            if (LanguageCode.Equals("en"))
            {
                var mc = from o in db.Organizations
                    join c in db.MenuCatalogs on o.ID equals c.OrganizationID
                    where o.ID == OrganizationId
                    select
                        new
                        {
                            c.ID,
                            ParentID = c.ParentID.Trim(),
                            MenuCatalogLocalizationName = c.CatalogLevelName.Trim()
                        };
                return mc.OrderBy(x => x.MenuCatalogLocalizationName);
            }
            else
            {
                var mc = from o in db.Organizations
                    join c in db.MenuCatalogs on o.ID equals c.OrganizationID
                    join l in db.MenuCatalogLocalizations on c.ID equals l.MenuCatalogID
                    join lang in db.Languages on l.LanguageID equals lang.ID
                    where o.ID == OrganizationId && lang.LanguageCode == LanguageCode
                    select
                        new
                        {
                            c.ID,
                            ParentID = c.ParentID.Trim(),
                            MenuCatalogLocalizationName = l.MenuCatalogLocalizationName.Trim()
                        };
                return mc.OrderBy(x => x.MenuCatalogLocalizationName);
            }
        }

        [Route("api/organization/{OrganizationId}/{LanguageCode}/MenuItem")]
        public IQueryable GetMenuItemByOrganization(Guid OrganizationId, string LanguageCode)
        {
            var mc = from it in db.MenuItems
                join c in db.MenuCatalogs on it.MenuCatalog_ID equals c.ID
                join l in db.MenuItemLocalizations on it.ID equals l.MenuItemID
                join lang in db.Languages on l.LanguageID equals lang.ID
                join o in db.Organizations on c.OrganizationID equals o.ID
                where o.ID == OrganizationId && lang.LanguageCode == LanguageCode
                     select new { c.ID, MenuCatalogID = c.ID,
                                  MenuItemLocalizationName = l.MenuItemLocalizationName.Trim(),
                                  it.MenuItemPrice,
                                  it.IsActive
                     }
            ;
            return mc.OrderBy(x => x.MenuItemLocalizationName);
        }

        [Route("api/organization/{OrganizationId}/OrganizationLogo")]
        public IQueryable GetImageByOrganization(Guid OrganizationId)
        {
            var img = from org in db.Organizations
                      where org.ID == OrganizationId
                select org.OrganizationLogo;
            return img;
        }

       
       

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(Guid id)
        {
            return db.Organizations.Count(e => e.ID == id) > 0;
        }
    }
}