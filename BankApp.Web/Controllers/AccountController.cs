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
        private readonly IAppUserRepository _repository;
        private readonly IAccountRepository _accountRepo;
        private readonly IAccountMapper _accountMapper;
        private readonly IUserMapper _mapper;

        public AccountController(IUserMapper mapper, IAppUserRepository repository, IAccountRepository accountRepo, IAccountMapper accountMapper)
        {
            _mapper = mapper;
            _repository = repository;
            _accountRepo = accountRepo;
            _accountMapper = accountMapper;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _mapper.MapToUserList(_repository.GetById(id));
            return View(userInfo);
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _accountRepo.Create(_accountMapper.Map(model));
            return RedirectToAction("Index", "Home");
        }
    }
}
