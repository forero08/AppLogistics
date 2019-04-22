﻿using AppLogistics.Components.Extensions;
using AppLogistics.Components.Security;
using AppLogistics.Data.Core;
using AppLogistics.Objects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AppLogistics.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IHasher _hasher;

        public AccountService(IUnitOfWork unitOfWork, IHasher hasher)
            : base(unitOfWork)
        {
            _hasher = hasher;
        }

        public TView Get<TView>(int id) where TView : BaseView
        {
            return UnitOfWork.GetAs<Account, TView>(id);
        }

        public IQueryable<AccountView> GetViews()
        {
            return UnitOfWork
                .Select<Account>()
                .To<AccountView>()
                .OrderByDescending(account => account.Id);
        }

        public bool IsLoggedIn(IPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }

        public bool IsActive(int id)
        {
            return UnitOfWork.Select<Account>().Any(account => account.Id == id && !account.IsLocked);
        }

        public string Recover(AccountRecoveryView view)
        {
            Account account = UnitOfWork.Select<Account>().SingleOrDefault(model => string.Equals(model.Email, view.Email, StringComparison.OrdinalIgnoreCase));
            if (account == null)
            {
                return null;
            }

            account.RecoveryTokenExpirationDate = DateTime.Now.AddMinutes(30);
            account.RecoveryToken = Guid.NewGuid().ToString();

            UnitOfWork.Update(account);
            UnitOfWork.Commit();

            return account.RecoveryToken;
        }

        public void Reset(AccountResetView view)
        {
            Account account = UnitOfWork.Select<Account>().Single(model => model.RecoveryToken == view.Token);
            account.Passhash = _hasher.HashPassword(view.NewPassword);
            account.RecoveryTokenExpirationDate = null;
            account.RecoveryToken = null;

            UnitOfWork.Update(account);
            UnitOfWork.Commit();
        }

        public void Create(AccountCreateView view)
        {
            Account account = UnitOfWork.To<Account>(view);
            account.Passhash = _hasher.HashPassword(view.Password);
            account.Email = view.Email.ToLower();

            UnitOfWork.Insert(account);
            UnitOfWork.Commit();
        }

        public void Edit(AccountEditView view)
        {
            Account account = UnitOfWork.Get<Account>(view.Id);
            account.IsLocked = view.IsLocked;
            account.RoleId = view.RoleId;

            UnitOfWork.Update(account);
            UnitOfWork.Commit();
        }

        public void Edit(ClaimsPrincipal user, ProfileEditView view)
        {
            Account account = UnitOfWork.Get<Account>(CurrentAccountId);
            account.Email = view.Email.ToLower();
            account.Username = view.Username;

            if (!string.IsNullOrWhiteSpace(view.NewPassword))
            {
                account.Passhash = _hasher.HashPassword(view.NewPassword);
            }

            UnitOfWork.Update(account);
            UnitOfWork.Commit();

            user.UpdateClaim(ClaimTypes.Name, account.Username);
            user.UpdateClaim(ClaimTypes.Email, account.Email);
        }

        public void Delete(int id)
        {
            UnitOfWork.Delete<Account>(id);
            UnitOfWork.Commit();
        }

        public async Task Login(HttpContext context, string username)
        {
            Account account = UnitOfWork.Select<Account>().Single(model => string.Equals(model.Username, username, StringComparison.OrdinalIgnoreCase));

            await context.SignInAsync("Cookies", new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email)
            }, "Password")));
        }

        public async Task Logout(HttpContext context)
        {
            await context.SignOutAsync("Cookies");
        }
    }
}
