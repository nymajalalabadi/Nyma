using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.User;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<User> GetUserForLogin(LoginUserViewModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName && u.Password == login.Password);
            return user;
        }
    }
}
