using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using BankApp.Web.Data.Repositories;
using BankApp.Web.Data.UnitOfWork;
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
        private readonly IUserMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUserMapper mapper)
        {
            
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.MappToListUserList(_unitOfWork.GetRepository<AppUser>().GetAll()));
        }
    }
}
