using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IEducationService : IAsyncDisposable
    {
        Task<Education> GetEducationById(long id);

        Task<List<EducationViewModel>> GetAllEducations();

        Task<bool> CreateOrEditEducation(CreateOrEditEducationViewModel education);

        Task<CreateOrEditEducationViewModel> FillCreateOrEditEducationViewModel(long id);

        Task<bool> DeleteEducation(long id);
    }
}
