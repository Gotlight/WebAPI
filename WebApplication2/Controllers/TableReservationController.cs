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
using System.Web.UI.WebControls;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TableReservationController : ApiController
    {
        private RESTAURANTEntities2 db = new RESTAURANTEntities2();

        // GET api/TableReservation
        public IQueryable<TableReservation> GetTableReservations()
        {
            return db.TableReservations;
        }

        // GET api/TableReservation/5
        [ResponseType(typeof(TableReservation))]
        public async Task<IHttpActionResult> GetTableReservation(Guid id)
        {
            TableReservation tablereservation = await db.TableReservations.FindAsync(id);
            if (tablereservation == null)
            {
                return NotFound();
            }

            return Ok(tablereservation);
        }

        // PUT api/TableReservation/5
        public async Task<IHttpActionResult> PutTableReservation(Guid id, TableReservation tablereservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tablereservation.ID)
            {
                return BadRequest();
            }

            db.Entry(tablereservation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableReservationExists(id))
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

        // POST api/TableReservation
        [ResponseType(typeof(TableReservation))]
        public async Task<IHttpActionResult> PostTableReservation(Reservation reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tablereservation = new TableReservation
            {
                ID = Guid.NewGuid(),
                AspNetUserID = reserve.AspNetUserID,
                OrganizationID = new Guid(reserve.OrganizationID),
                PersonsCount = reserve.PersonsCount,
                ReservationDateTime = reserve.ReservationDateTime
            };


            db.TableReservations.Add(tablereservation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (TableReservationExists(tablereservation.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tablereservation.ID }, tablereservation);
        }

        // DELETE api/TableReservation/5
        [ResponseType(typeof(TableReservation))]
        public async Task<IHttpActionResult> DeleteTableReservation(Guid id)
        {
            TableReservation tablereservation = await db.TableReservations.FindAsync(id);
            if (tablereservation == null)
            {
                return NotFound();
            }

            db.TableReservations.Remove(tablereservation);
            await db.SaveChangesAsync();

            return Ok(tablereservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableReservationExists(Guid id)
        {
            return db.TableReservations.Count(e => e.ID == id) > 0;
        }
    }
}