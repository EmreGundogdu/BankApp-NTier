using BankApp.Web.Data.Entities;
using BankApp.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Web.Mapping
{
    public class AppUserMapper:IUserMapper
    {
        public List<UserListModel> MappToListUserList(List<AppUser> users)
        {
            return users.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname
            }).ToList();
        }
        public UserListModel MapToUserList(AppUser user)
        {
            return new UserListModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
