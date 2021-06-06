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
        //private readonly IAppUserRepository _repository;
        //private readonly IAccountRepository _accountRepo;
        //private readonly IAccountMapper _accountMapper;
        //private readonly IUserMapper _mapper;

        //public AccountController(IUserMapper mapper, IAppUserRepository repository, IAccountRepository accountRepo, IAccountMapper accountMapper)
        //{
        //    _mapper = mapper;
        //    _repository = repository;
        //    _accountRepo = accountRepo;
        //    _accountMapper = accountMapper;
        //}
        private readonly IGenericRepository<Account> _accounRepository;
        private readonly IGenericRepository<AppUser> _appUserRepository;

        public AccountController(IGenericRepository<Account> accounRepository, IGenericRepository<AppUser> appUserRepository)
        {
            _accounRepository = accounRepository;
            _appUserRepository = appUserRepository;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _appUserRepository.GetById(id);
            return View(new UserListModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                Surname = userInfo.Surname
            });
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _accounRepository.Create(new Account
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                AppUserId = model.AppUserId
            });
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _accounRepository.GetQuaryable();
            var accounts = query.Where(x => x.AppUserId == userId).ToList();
            var user = _appUserRepository.GetById(userId);
            ViewBag.Fullname = user.Name + " " + user.Surname;
            var list = new List<AccountListModel>();
            foreach (var item in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = item.AccountNumber,
                    ApplicationUserId = item.AppUserId,
                    Balance = item.Balance,

                    Id = item.Id
                });
            }
            return View(list);
        }
    }
}
