﻿using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jotfi.Jot.Core.ViewModels.System
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public void Run()
        {
            if (!GetDatabase().CheckTables(GetTableNames()))
            {
                return;
            }
            if (!CheckAdministrator(out string error))
            {
                if (!string.IsNullOrEmpty(error))
                {
                    GetApp().ShowError(error);
                }
                return;
            }
            if (!CheckOrganization())
            {
                return;
            }
            while (!GetApp().IsLoggedIn())
            {
                if (!GetViewModels().Login.PerformLogin())
                {
                    break;
                }
            }
            if (!GetApp().IsLoggedIn())
            {
                return;
            }
            GetViews().Start.ApplicationStart();
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return GetRepository().System.TableNames.GetList(whereConditions).ToList();
        }

        bool CheckAdministrator(out string error)
        {
            error = string.Empty;
            if (GetRepository().System.Users.Exists())
            {
                return true;
            }
            var admin = new User("Administrator", "System Administrator");
            return GetViews().Start.SetupAdministrator(admin, out error);
        }

        public bool IsAdministratorValid(User user, out string error)
        {
            error = string.Empty;
            if (!GetPasswordValid(user.Password.CreatePassword))
            {
                error += "Invalid password. Password must not be too weak.\r\n";
                error += GetPasswordScoreInfo(user.Password.CreatePassword);
                return false;
            }
            if (user.Password.CreatePassword != user.Password.ConfirmPassword)
            {
                error += "Invalid password. Confirm password does not match.";
                return false;
            }
            if (!GetEmailValid(user.Person.Email.EmailAddress))
            {
                error += "Invalid email. Please check email address.";
                return false;
            }
            if (user.Person.Email.EmailAddress != user.Person.Email.ConfirmEmail)
            {
                error += "Invalid email. Confirm email does not match.";
                return false;
            }
            return true;
        }

        public bool SaveAdministrator(User admin, out string error)
        {
            if (!IsAdministratorValid(admin, out error))
            {
                return false;
            }

            return true;
        }

        bool CheckOrganization()
        {
            if (GetRepository().System.Organizations.Exists())
            {
                return true;
            }
            return GetViews().Start.SetupOrganization();
        }

        public string CreateAdministratorText()
        {
            return $@"
Setting up {Constants.DefaultApplicationName} for the first time.
To get started, an Administrator account with full access will be created.
This account should only be used for system administration.";
        }

        public bool GetPasswordValid(string password)
        {
            var passwordScore = PasswordAdvisor.CheckStrength(password);
            return passwordScore switch
            {
                PasswordScore.Blank => false,
                PasswordScore.VeryWeak => false,
                PasswordScore.Weak => false,
                _ => true
            };
        }

        public string GetPasswordScoreInfo(string password)
        {
            var passwordScore = PasswordAdvisor.CheckStrength(password);
            var passwordInfo = $"Password Strength: {passwordScore}";
            switch (passwordScore)
            {
                case PasswordScore.Blank:
                case PasswordScore.VeryWeak:
                case PasswordScore.Weak:
                    passwordInfo += ", must be at least 8 characters.";
                    break;
                case PasswordScore.Medium:
                case PasswordScore.Strong:
                case PasswordScore.VeryStrong:
                    // Password deemed strong enough, allow user to be added to database etc
                    break;
            }
            return passwordInfo;
        }

        public bool GetEmailValid(string email)
        {
            return EmailValidator.IsEmailValid(email);
        }
    }
}
