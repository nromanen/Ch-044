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

        #region signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDataFromSocial()
        {
            var uLoginUser = ULoginHelper.GetULoginUser(
              this.Request.Form["token"],
              this.Request.ServerVariables["SERVER_NAME"]);

            UserDTO userDTO = new UserDTO()
            {
                UserName = ULoginHelper.GetName(uLoginUser),
                Email = uLoginUser.Email,
                SocialNetwork = uLoginUser.Network,
                Password = uLoginUser.Uid,
                ConfirmPassword = uLoginUser.Uid
            };

            if (userDTO.Email == null)
                return View("EnterEmail", userDTO);

            return SocialNetworkSignUp(userDTO);
        }

        [HttpPost]
        public ActionResult SocialNetworkSignUp(UserDTO user)
        {
            if (UserManager.EmailIsExist(user.Email))
            {
                ModelState.AddModelError("Email", Resources.Resource.EmailExist);
                return View("EnterEmail", user);
            }
            if (!ModelState.IsValid || user.SocialNetwork == null)
            {
                return View("SocialNetworkErrMessage");
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
        #endregion

        #region login

        [HttpPost]
        public ActionResult SocialNetworkLogin()
        {
            var uLoginUser = ULoginHelper.GetULoginUser(
                this.Request.Form["token"],
                this.Request.ServerVariables["SERVER_NAME"]);

            UserDTO user = UserManager.GetSocialNetworkUser(uLoginUser.Uid, uLoginUser.Network);

            if (user == null)
            {
                return View("SocialNetworkErrMessage");
            }

            var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
            claim.AddClaim(
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName, ClaimValueTypes.String));

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, claim);
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
                UserDTO user = UserManager.GetUser(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("Email", Resources.Resource.UncorrectEmailPassword);
                    ModelState.AddModelError("Password", Resources.Resource.UncorrectEmailPassword);
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "OWIN Provider", ClaimValueTypes.String));
                    // TODO: Permissions will be separate story. if (user.Role != null) claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        #endregion

        #region logout
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}