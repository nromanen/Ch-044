using BAL.Interface;
using Microsoft.Owin.Security;
using Model.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : BaseController
    {
        private IUserManager UserManager;

        public AccountController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        [HttpPost]
        public ActionResult GetDataFromNetwork()
        {
            var uLoginUser = ULoginHelper.GetULoginUser(
              this.Request.Form["token"],
              this.Request.ServerVariables["SERVER_NAME"]);

            //If user account is already exist redirect to login method
            if (UserManager.NetworkAccountExict(uLoginUser.Uid, uLoginUser.Network))
                return NetworkLogin();

            var user = new NetworkUserDTO()
            {
                UserName = ULoginHelper.GetName(uLoginUser),
                Email = uLoginUser.Email,
                Network = uLoginUser.Network,
                NetworkAccountId = uLoginUser.Uid
            };

            if (user.Email == null)
                return View("EnterEmail", user);

            return NetworkSignUp(user);
        }

        [HttpPost]
        public ActionResult NetworkSignUp(NetworkUserDTO user)
        {
            if (UserManager.EmailIsExist(user.Email))
            {
                ModelState.AddModelError("Email", Resources.Resource.EmailExist);
                return View("EnterEmail", user);
            }
            if (!ModelState.IsValid || user.Network == null)
            {
                return View("NetworkErrMessage");
            }

            UserManager.Insert(user);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View(new UserDTO());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserDTO user)
        {
            if (user.Password == null)
            {
                ModelState.AddModelError("Password", Resources.Resource.InputPassword);
            }
            if (UserManager.EmailIsExist(user.Email))
            {
                ModelState.AddModelError("Email", Resources.Resource.EmailExist);
            }
            if (!ModelState.IsValid) return View(user);

            UserManager.Insert(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult NetworkLogin()
        {
            var uLoginUser = ULoginHelper.GetULoginUser(
                this.Request.Form["token"],
                this.Request.ServerVariables["SERVER_NAME"]);

            NetworkUserDTO user = UserManager.GetNetworkUser(uLoginUser.Uid, uLoginUser.Network);

            if (user == null)
            {
                return View("NetworkErrMessage");
            }

            var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
            claim.AddClaim(
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName, ClaimValueTypes.String));

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = UserManager.GetByEmail(model.Name, model.Password) ?? UserManager.GetByUserName(model.Name, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("Name", Resources.Resource.UncorrectEmailPassword);
                    ModelState.AddModelError("Password", Resources.Resource.UncorrectEmailPassword);
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
                    claim.AddClaim(
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "OWIN Provider", ClaimValueTypes.String));
                    // TODO: Permissions will be separate story. if (user.Role != null) claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


      
    }
}