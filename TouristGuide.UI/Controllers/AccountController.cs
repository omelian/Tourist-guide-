using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Enums;
using TouristGuide.UI.Models;
using System.IO;
using Amazon.CloudSearchDomain;
using System.Net.Mail;
using NLog;


namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Authorization system class
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// User Bl 
        /// </summary>
        private IUnitOfWork unitOfWork;
        ///// <summary>
        ///// Information about claim  
        ///// </summary>
        //ClaimsPrincipal principal = null;
        ///// <summary>
        /// Claim
        /// </summary>
        ClaimsIdentity claim = null;


        /// <summary>
        /// verifing Email
        /// </summary>
        private static string verifingEmail = string.Empty;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Instantiate the class that holds the user logic via dependency injection
        /// </summary>
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Manages the authentication modules called during the client authentication process.
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// GET request for login page 
        /// </summary>
        /// <returns>Display the user page</returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View("Login");
        }


        /// <summary>
        /// Create random String
        /// </summary>    
        /// <returns>string as filename for amazon</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Authenticate user in the system
        /// </summary>
        /// <param name="model">View model</param>
        /// <returns>Redirect user to the main page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserLoginModel userLoginModel = new UserLoginModel { Email = model.Email, Password = model.Password };
                claim = this.unitOfWork.UserBL.Authenticate(userLoginModel);
                if (claim == null)
                {
                    ViewBag.ReturnURL = returnUrl;
                    logger.Error("Claim equals null.Incorrect login or password.");
                    ModelState.AddModelError(string.Empty, "Incorrect login or password");
                }
                else
                if (claim.Claims.Where(c => (c.Type == ClaimValueTypes.String)).Select(c => c.Value).SingleOrDefault() != String.Empty)
                {

                    ModelState.AddModelError(string.Empty, claim.Claims.Where(c => (c.Type == ClaimValueTypes.String)).Select(c => c.Value).SingleOrDefault());
                }
                else
                {
                    this.AuthenticationManager.SignOut();
                    this.AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, this.claim);
                    if (claim.Claims.Where(c => (c.Type == ClaimValueTypes.KeyInfo)).Select(c => c.Value).FirstOrDefault() == UserRoleEnum.SuperAdmin.ToString())
                    {
                        return RedirectToAction("UsersTable", "SuperAdmin");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            if (!returnUrl.Contains("Register"))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
                return View(model);

            }
            else
            {
                return View(model);
            }

        }

        /// <summary>
        /// Gets users claim 
        /// </summary>
        /// <returns>JSON Result with claims data</returns>
        public JsonResult GetClaim()
        {
            AuthenticationModel currentUser = null;
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                IPrincipal currentPrincipal = Thread.CurrentPrincipal;
                currentUser = new AuthenticationModel
                {
                    Name = currentPrincipal.Identity.Name
                };
                if (currentPrincipal.IsInRole(UserRoleEnum.User.ToString()))
                {
                    currentUser.Role = UserRoleEnum.User.ToString();

                }
                else if (currentPrincipal.IsInRole(UserRoleEnum.Moderator.ToString()))
                {
                    currentUser.Role = UserRoleEnum.Moderator.ToString();
                }
                else if (currentPrincipal.IsInRole(UserRoleEnum.Admin.ToString()))
                {
                    currentUser.Role = UserRoleEnum.Admin.ToString();
                }
                else
                {
                    currentUser.Role = UserRoleEnum.SuperAdmin.ToString();
                }
                currentUser.Id = User.Identity.GetUserId();

                return this.Json(currentUser, JsonRequestBehavior.AllowGet);
            }

            currentUser = new AuthenticationModel
            {
                Id = "noId",
                Role = "Guest",
                Name = "Guest",
            };

            return this.Json(currentUser, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sign out user from his account
        /// </summary>
        /// <returns>Redirect to main page</returns>
        public ActionResult Logout()
        {
            Thread.CurrentPrincipal = null;
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            UserRegisterModel model = new UserRegisterModel();
            ViewBag.ReturnUrl = returnUrl;
            return View("Register", model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = this.unitOfWork.UserBL.Create(model);
                if (result.Successfully )
                {
                    UserLoginModel loginModel = new UserLoginModel();
                    loginModel.Email = model.Email;
                    loginModel.Password = model.Password;
                    if(model.Role != UserRoleEnum.Admin)
                    {
                        claim = this.unitOfWork.UserBL.Authenticate(loginModel);
                        this.AuthenticationManager.SignOut();
                        this.AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, this.claim);
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
                    }
                }
                else
                {
                    logger.Error("Unsuccessful registration operation");
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            return View(model);
        }

        //-----------------------------------------------------------------------------------------------------
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            ControllerContext.HttpContext.Session.RemoveAll();
            
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }



        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                logger.Warn("External login callback.Login info equlas null.");
                return RedirectToAction("Login");
            }
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);         
            
            switch (result)
            {
                case SignInStatus.Success:
                    {   
                        string userId = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId();                        
                        this.claim = this.unitOfWork.UserBL.AuthenticateExternal(userId);
                        if (claim != null)
                        {
                            this.AuthenticationManager.SignOut();
                            this.AuthenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = true
                            }, this.claim);
                            if (claim.Claims.Where(c => (c.Type == ClaimValueTypes.KeyInfo)).Select(c => c.Value).FirstOrDefault() == UserRoleEnum.SuperAdmin.ToString())
                            {
                                return RedirectToAction("UsersTable", "SuperAdmin");
                            }
                            else
                            {
                                return RedirectToLocal(returnUrl);
                            }    
                        }
                        return RedirectToAction("Login");           
                    }                    
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }
            if (ModelState.IsValid)
            {
               
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = this.unitOfWork.UserBL.CreateExternal(model.Email, info);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);                    
                    this.claim = this.unitOfWork.UserBL.AuthenticateExternal(user.Id);
                    if (claim != null)
                    {
                        this.AuthenticationManager.SignOut();
                        this.AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, this.claim);
                        if (claim.Claims.Where(c => (c.Type == ClaimValueTypes.KeyInfo)).Select(c => c.Value).FirstOrDefault() == UserRoleEnum.SuperAdmin.ToString())
                        {
                            return RedirectToAction("UsersTable", "SuperAdmin");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                }               
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        
        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = this.unitOfWork.UserBL.IsUserInDataBase(model.Email);

                string code = RandomString(10);

                if (result.Successfully)
                {
                    this.unitOfWork.UserBL.SetCodeForEmail(code, model.Email);
                    verifingEmail = model.Email;
                    string textForEmail = "Hi! Here is a code to reset your password on site http://travellertourist.azurewebsites.net/ , please type this code in special field on our website." + Environment.NewLine + "Code:" + code;
                    SendMail("smtp.gmail.com", "traveleradm1@gmail.com", "Qwer27512*", verifingEmail, "Register in traveler", textForEmail, null);
                        //send code here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    return RedirectToAction("VerifyCode", "Account");     
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult VerifyCode()
        {
            VerifyCodeViewModel model = new VerifyCodeViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult VerifyCode(VerifyCodeViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.Email = verifingEmail;
                var result = this.unitOfWork.UserBL.IsUserInDataBase(model.Email);
                if (result.Successfully)
                {
                    var resultcode = this.unitOfWork.UserBL.IsCodeValid(model.Code, model.Email);
                    if (resultcode.Successfully)
                    {
                        return RedirectToAction("ResetPassword", "Account");
                    }
                    else
                    {
                        logger.Error("Incorrect verifying code.");
                        ModelState.AddModelError("", resultcode.Message);
                    }
                }
                else
                {
                    logger.Error("Incorrect user email");
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            model.Email = verifingEmail;
            var result = this.unitOfWork.UserBL.IsUserInDataBase(model.Email);
                if (result.Successfully)
                {
                    this.unitOfWork.UserBL.ResetPasswordByEmail(model.Email, model.Password);
                    UserLoginModel loginModel = new UserLoginModel();
                    loginModel.Email = model.Email;
                    loginModel.Password = model.Password;
                    claim = this.unitOfWork.UserBL.Authenticate(loginModel);
                    if (claim.Claims.Where(c => (c.Type == ClaimValueTypes.KeyInfo)).Select(c => c.Value).FirstOrDefault() != UserRoleEnum.Admin.ToString())
                    {
                        this.AuthenticationManager.SignOut();
                        this.AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, this.claim);

                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    logger.Error("Incorrect user email");
                    ModelState.AddModelError("", result.Message);
                }

            return View(model);
        }


        /// <summary>
        /// Send email to user
        /// </summary>
        /// <param name="smtpServer">smtpServer</param>
        /// <param name="from">from</param>
        /// <param name="password">password</param>
        /// <param name="mailto">mailto</param>
        /// <param name="caption">caption</param>
        /// <param name="message">message</param>
        /// <param name="attachFile">attachFile</param>
        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                {
                    mail.Attachments.Add(new Attachment(attachFile));
                }

                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {               
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}