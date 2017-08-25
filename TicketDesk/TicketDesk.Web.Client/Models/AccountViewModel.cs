﻿// TicketDesk - Attribution notice
// Contributor(s):
//
//      Stephen Redd (https://github.com/stephenredd)
//
// This file is distributed under the terms of the Microsoft Public 
// License (Ms-PL). See http://opensource.org/licenses/MS-PL
// for the complete terms of use. 
//
// For any distribution that contains code from this file, this notice of 
// attribution must remain intact, and a copy of the license must be 
// provided to the recipient.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using TicketDesk.Localization;
using TicketDesk.Localization.Models;
using TicketDesk.Web.Identity;
using TicketDesk.Web.Identity.Model;

namespace TicketDesk.Web.Client.Models
{
    public class AccountPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Validation_sq))]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Strings_sq))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Validation_sq))]
        [StringLength(100, ErrorMessageResourceName = "FieldMinimumLength", ErrorMessageResourceType = typeof(Validation_sq), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Strings_sq))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Strings_sq))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceName = "NewConfirmationDoNotMatch", ErrorMessageResourceType = typeof(Strings_sq))]
        public string ConfirmPassword { get; set; }
    }

    public class AccountProfileViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Validation_sq))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Validation_sq))]
        [Display(Name = "Email", ResourceType = typeof(Strings_sq))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Validation_sq))]
        [StringLength(100, ErrorMessageResourceName = "FieldMaximumLength", ErrorMessageResourceType = typeof(Validation_sq))]
        [Display(Name = "DisplayName", ResourceType = typeof(Strings_sq))]
        public string DisplayName { get; set; }
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Validation_sq))]
        [StringLength(100, ErrorMessageResourceName = "FieldMaximumLength", ErrorMessageResourceType = typeof(Validation_sq))]
        [Display(Name = "Phone", ResourceType = typeof(Strings_sq))]
        public string Phone { get; set; }
    }

    public class UserAccountInfoViewModel
    {
        public UserAccountInfoViewModel()
        {
            Roles = new string[] { };
        }

        public UserAccountInfoViewModel(TicketDeskUser user, IEnumerable<string> roles)
        {
            var lockDate = (user.LockoutEndDateUtc ?? DateTime.MinValue).ToUniversalTime();
            User = user;
            IsLocked = lockDate > DateTime.UtcNow && lockDate < DateTime.MaxValue.ToUniversalTime();
            IsDisabled = lockDate == DateTime.MaxValue.ToUniversalTime();
            Roles = roles ?? new string[] { };
        }

        public TicketDeskUser User { get; set; }

        [Display(Name = "Locked", Prompt = "Locked_Prompt", ResourceType = typeof(Strings_sq))]
        [LocalizedDescription("IsLocked_Description", NameResourceType = typeof(Strings_sq))]
        public bool IsLocked { get; set; }

        [Display(Name = "Disabled", Prompt = "Disabled_Prompt", ResourceType = typeof(Strings_sq))]
        [LocalizedDescription("IsDisabled_Description", NameResourceType = typeof(Strings_sq))]
        public bool IsDisabled { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<string> GetRoleNames(IEnumerable<TicketDeskRole> allRolesList)
        {
            return allRolesList.Where(ar => Roles.Any(r => r == ar.Id)).Select(ar => ar.DisplayName);
        }

        public MultiSelectList UserRolesList
        {
            get
            {
                var roleManager = DependencyResolver.Current.GetService<TicketDeskRoleManager>();

                return roleManager.Roles.ToMultiSelectList(
                    r => r.Id,
                    r => r.DisplayName,
                    Roles.ToArray());
            }
        }


    }
}