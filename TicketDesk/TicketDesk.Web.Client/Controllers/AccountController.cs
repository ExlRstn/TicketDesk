﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketDesk.Localization.Controllers;
using TicketDesk.PushNotifications;
using TicketDesk.PushNotifications.Model;
using TicketDesk.Web.Client.Models;
using TicketDesk.Web.Identity;
using TicketDesk.Web.Identity.Model;

namespace TicketDesk.Web.Client.Controllers
{
    [TdAuthorizeAttribute]
    [RoutePrefix("account")]
    [Route("{action=manage}")]
    public class AccountController : Controller
    {
        private TicketDeskUserManager UserManager { get; set; }
        private TicketDeskSignInManager SignInManager { get; set; }
        private TdPushNotificationContext NotificationContext { get; set; }
        public AccountController(TicketDeskUserManager userManager, TicketDeskSignInManager signInManager, TdPushNotificationContext notificationContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            NotificationContext = notificationContext;
        }


        [Route("manage")]
        public ActionResult Manage(AccountMessageId? message)
        {
            ViewBag.StatusMessage =
               message == AccountMessageId.ChangePasswordSuccess ? Strings_sq.ChangePasswordSuccess
               : message == AccountMessageId.Error ? Strings_sq.Error
               : message == AccountMessageId.ProfileSaveSuccess ? Strings_sq.ProfileSaveSuccess
               : "";
            return View();
        }

        [Route("change-password")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("change-password")]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(AccountPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var demoMode = (ConfigurationManager.AppSettings["ticketdesk:DemoModeEnabled"] ?? "false").Equals("true", StringComparison.InvariantCultureIgnoreCase);
            if (demoMode)
            {
                ModelState.AddModelError("Password", Strings_sq.UnableToChangeDemoUser);
            }
            else
            {
                var result =
                        await
                            UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Manage", new { Message = AccountMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
            }

            return View(model);
        }

        [Route("edit-profile")]
        public ActionResult EditProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            
            var model = new AccountProfileViewModel { DisplayName = User.Identity.GetUserDisplayName(), Email = User.Identity.GetUserName(), Phone = user.PhoneNumber };
            return View(model);
        }

        [Route("edit-profile")]
        [HttpPost]
        public async Task<ActionResult> EditProfile(AccountProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var oldEmail = user.Email;

            var demoMode = (ConfigurationManager.AppSettings["ticketdesk:DemoModeEnabled"] ?? "false").Equals("true", StringComparison.InvariantCultureIgnoreCase);
            if (demoMode && oldEmail.EndsWith("@example.com", StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("Email", Strings_sq.UnableToChangeDemoUser);
            }
            else
            {

                user.UserName = model.Email;
                user.Email = model.Email;
                user.DisplayName = model.DisplayName;
                user.PhoneNumber = model.Phone;
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await ResetMailEmailDestination(user, oldEmail);

                    AuthenticationManager.SignOut();
                    await SignInManager.SignInAsync(user, false, false);
                    //.SignIn(new AuthenticationProperties { IsPersistent = false }, 
                    //await user.GenerateUserIdentityAsync(UserManager);
                    return RedirectToAction("Manage", new { Message = AccountMessageId.ProfileSaveSuccess });
                }
            }
            return View(model);
        }

        private async Task ResetMailEmailDestination(TicketDeskUser user, string oldEmail)
        {
            var noteSettings =
                await NotificationContext.SubscriberPushNotificationSettingsManager.GetSettingsForSubscriberAsync(user.Id);
            if (noteSettings != null)
            {
                var dest = noteSettings.PushNotificationDestinations.FirstOrDefault(
                    d => d.DestinationType == "email" && d.DestinationAddress == oldEmail);
                if (dest == null)
                {
                    dest = new PushNotificationDestination() { SubscriberId = user.Id, DestinationType = "email" };
                    noteSettings.PushNotificationDestinations.Add(dest);
                }
                dest.DestinationAddress = user.Email;
                dest.SubscriberName = user.DisplayName;
                await NotificationContext.SaveChangesAsync();
            }
        }

        public enum AccountMessageId
        {
            ChangePasswordSuccess,
            ProfileSaveSuccess,
            Error
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(TicketDeskUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}