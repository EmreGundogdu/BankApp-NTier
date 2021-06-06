using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Data.UnitOfWork;
using BankApp.Web.Mapping;
using BankApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //private readonly IGenericRepository<Account> _accounRepository;
        //private readonly IGenericRepository<AppUser> _appUserRepository;

        //public AccountController(IGenericRepository<Account> accounRepository, IGenericRepository<AppUser> appUserRepository)
        //{
        //    _accounRepository = accounRepository;
        //    _appUserRepository = appUserRepository;
        //}

        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _unitOfWork.GetRepository<AppUser>().GetById(id);
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
            _unitOfWork.GetRepository<Account>().Create(new Account
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                AppUserId = model.AppUserId
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _unitOfWork.GetRepository<Account>().GetQuaryable();
            var accounts = query.Where(x => x.AppUserId == userId).ToList();
            var user = _unitOfWork.GetRepository<AppUser>().GetById(userId);
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
        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _unitOfWork.GetRepository<Account>().GetQuaryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();
            var list = new List<AccountListModel>();
            ViewBag.SenderId = accountId;
            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.AppUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }
            return View(new SelectList(list, "Id", "AccountNumber"));
        }
        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            var senderAccount = _unitOfWork.GetRepository<Account>().GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _unitOfWork.GetRepository<Account>().Update(senderAccount);
            var account = _unitOfWork.GetRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _unitOfWork.GetRepository<Account>().Update(account);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
