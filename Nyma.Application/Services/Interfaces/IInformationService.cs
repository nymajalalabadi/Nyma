using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IInformationService : IAsyncDisposable
    {
        Task<InformationViewModel> GetAllInformation();

        Task<Information> GetInformationModel();

        Task<CreateOrEditInformationViewModel> FillCreateOrEditInformationViewModel();

        Task<bool> CreateOrEditInformation(CreateOrEditInformationViewModel information);
    }
}
