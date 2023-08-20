using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserForLogin(LoginUserViewModel login);
    }
}
