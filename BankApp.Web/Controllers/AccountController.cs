using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Mapping;
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
        private readonly IAppUserRepository _repository;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserMapper _mapper;

        public AccountController(BankContext context, IUserMapper mapper, IAppUserRepository repository, IAccountRepository accountRepo)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
            _accountRepo = accountRepo;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _mapper.MapToUserList(_repository.GetById(id));
            return View(userInfo);
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _accountRepo.Create(_mapper.)
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
