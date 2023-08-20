using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IExperienceService : IAsyncDisposable
    {
        Task<Experience> GetExperienceById(long Id);

        Task<List<ExperienceViewModel>> GetAllExperiences();

        Task<bool> CreateOrEditExperience(CreateOrEditExperienceViewModel experience);

        Task<CreateOrEditExperienceViewModel> FillCreateOrEditExperienceViewModel(long id);

        Task<bool> DeleteExperience(long id);
    }
}
