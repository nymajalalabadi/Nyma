using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.CustomerFeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface ICustomerFeedBackService : IAsyncDisposable
    {
        Task<CustomerFeedBack> GetCustomerFeedBackById(long id);

        Task<List<CustomerFeedBackViewModel>> GetCustomerFeedBackForIndex();

        Task<CreateOrEditCustomerFeedbackViewModel> FillCreateOrEditCustomerFeedbackViewModel(long id);

        Task<bool> CreateOrEditCustomerFeedback(CreateOrEditCustomerFeedbackViewModel customerFeedback);

        Task<bool> DeleteCustomerFeedback(long id);
    }
}
