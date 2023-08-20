using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.CustomerLogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface ICustomerLogoService : IAsyncDisposable
    {
        Task<CustomerLogo> GetCustomerLogoById(long id);

        Task<List<CustomerLogoViewModel>> GetCustomerLogoForIndex();

        Task<CreateOrEditCustomerLogoViewModel> FillCreateOrEditCustomerLogoViewModel(long id);

        Task<bool> CreateOrEditCustomerLogo(CreateOrEditCustomerLogoViewModel customerLogo);

        Task<bool> DeleteCustomerLogo(long id);
    }
}
