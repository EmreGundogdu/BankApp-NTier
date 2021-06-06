using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankContext _context;
        private readonly AppUserRepository _repository;

        public AccountController(BankContext context, AppUserRepository repository)
        {
            _context = context;
            _repository = new AppUserRepository(_context);
        }

        public IActionResult Create(int id)
        {
            var userInfo = _repository.GetById(id);
            return View(userInfo);
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _context.Accounts.Add(new Account
            {
                AccountNumber = model.AccountNumber,
                AppUserId = model.AppUserId,
                Balance = model.Balance
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
