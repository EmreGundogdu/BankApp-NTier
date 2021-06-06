using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly BankContext _context;

        public AppUserRepository(BankContext context)
        {
            _context = context;
        }

        public List<AppUser> GetAll()
        {
            return _context.AppUsers.ToList();
        }
        public AppUser GetById(int id)
        {
            return _context.AppUsers.FirstOrDefault(x => x.Id == id);
        }
        public void Create(AppUser user)
        {
            _context.Set<AppUser>().Add(user);
            _context.SaveChanges();
        }
        public void Remove(AppUser user)
        {
            _context.Set<AppUser>().Remove(user);
            _context.SaveChanges();
        }
    }
}
