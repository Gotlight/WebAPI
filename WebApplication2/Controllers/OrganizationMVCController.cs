using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class OrganizationMVCController : Controller
    {
        private RESTAURANTEntities2 db = new RESTAURANTEntities2();

        // GET: /OrganizationMVC/
        public async Task<ActionResult> Index()
        {
            var organizations = db.Organizations.Include(o => o.OrganizationType);
            return View(await organizations.ToListAsync());
        }

        // GET: /OrganizationMVC/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: /OrganizationMVC/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationTypeID = new SelectList(db.OrganizationTypes, "ID", "OrganizationTypeName");
            return View();
        }

        // POST: /OrganizationMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,OrganizationName,OrganizationTypeID,OrganizationAddress,IsActive,OrganizationLogo")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.ID = Guid.NewGuid();
                db.Organizations.Add(organization);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationTypeID = new SelectList(db.OrganizationTypes, "ID", "OrganizationTypeName", organization.OrganizationTypeID);
            return View(organization);
        }

        // GET: /OrganizationMVC/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationTypeID = new SelectList(db.OrganizationTypes, "ID", "OrganizationTypeName", organization.OrganizationTypeID);
            return View(organization);
        }

        // POST: /OrganizationMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,OrganizationName,OrganizationTypeID,OrganizationAddress,IsActive,OrganizationLogo")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationTypeID = new SelectList(db.OrganizationTypes, "ID", "OrganizationTypeName", organization.OrganizationTypeID);
            return View(organization);
        }

        // GET: /OrganizationMVC/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: /OrganizationMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Organization organization = await db.Organizations.FindAsync(id);
            db.Organizations.Remove(organization);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
