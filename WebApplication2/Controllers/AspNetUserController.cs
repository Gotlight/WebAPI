using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{


    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AspNetUserController : ApiController
    {
        private RESTAURANTEntities2 db = new RESTAURANTEntities2();

        public AspNetUserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AspNetUserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        // GET api/AspNetUser
        public IQueryable<AspNetUser> GetAspNetUsers()
        {
            return db.AspNetUsers;
        }

        // GET api/AspNetUser/5
        [Authorize]
        [ResponseType(typeof(AspNetUser))]
        public async Task<IHttpActionResult> GetAspNetUser(string id)
        {
//            if (User.Identity.GetUserId().Equals(id))
//            {
                AspNetUser aspnetuser = await db.AspNetUsers.FindAsync(id);
                if (aspnetuser == null)
                {
                    return NotFound();
                }

                return Ok(aspnetuser);
//            }
//            return null;
        }

        // PUT api/AspNetUser/5
        public async Task<IHttpActionResult> PutAspNetUser(string id, AspNetUser aspnetuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspnetuser.Id)
            {
                return BadRequest();
            }

            db.Entry(aspnetuser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(id))
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

        // POST api/AspNetUser
        //        [ResponseType(typeof(AspNetUser))]

        [System.Web.Http.HttpPost]
        [Route("api/AspNetUser/Register")]
        public async Task<IHttpActionResult> PostAspNetUser(User usr)
        {
            //            var aspnetuser = new AspNetUser {UserName = usr.username};
            var user = new ApplicationUser() { UserName = usr.username };
            var result = await UserManager.CreateAsync(user, usr.password);
            //            db.AspNetUsers.Add(aspnetuser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: true);
            }
            else
            {
                return BadRequest();
            }
//          user.SecurityStamp = 
            return Ok(user);


        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/AspNetUser/Login")]
        public async Task<IHttpActionResult> Login(User usr)
        {
            
                var user = await UserManager.FindAsync(usr.username, usr.password);
                if (user != null && AspNetUserExists(user.Id))
                {
                    await SignInAsync(user, isPersistent:true);
                    return Ok(user);
                }
                    return BadRequest();
        }

//        // DELETE api/AspNetUser/5
//        [ResponseType(typeof(AspNetUser))]
//        public async Task<IHttpActionResult> DeleteAspNetUser(string id)
//        {
//            AspNetUser aspnetuser = await UserManager.FindAsync(id);
//            if (aspnetuser == null)
//            {
//                return NotFound();
//            }
//
//            db.AspNetUsers.Remove(aspnetuser);
//            await db.SaveChangesAsync();
//
//            return Ok(aspnetuser);
//        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Id == id) > 0;
        }
    }
}