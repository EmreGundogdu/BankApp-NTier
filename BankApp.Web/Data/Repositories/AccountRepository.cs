using BankApp.Web.Data.Context;
using BankApp.Web.Data.Entities;
using BankApp.Web.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public void Create(Account account)
        {
            _context.Set<Account>().Add(account);
            _context.SaveChanges();
        }
        public void Remove(Account account)
        {
            _context.Set<Account>().Remove(account);
            _context.SaveChanges();
        }
        public List<Account> GettAl()
        {
            return _context.Set<Account>().ToList();
        }
    }
}
