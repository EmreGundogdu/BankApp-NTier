using BankApp.Web.Data.Context;
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
    public class HomeController : Controller
    {
        private readonly BankContext _context;
        private readonly IAppUserRepository _repository;
        private readonly IUserMapper _mapper;

        public HomeController(BankContext bankContext, IAppUserRepository repository, IUserMapper mapper)
        {
            _context = bankContext;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.MappToListUserList(_repository.GetAll()));
        }
    }
}
