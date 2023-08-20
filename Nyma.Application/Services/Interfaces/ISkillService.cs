using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface ISkillService :  IAsyncDisposable
    {
        Task<Skill> GetSkillById(long id);

       Task<List<SkillViewModel>> GetAllSkills();

        Task<bool> CreateOrEditSkill(CreateOrEditSkillViewModel skill);

        Task<CreateOrEditSkillViewModel> FillCreateOrEditSkillViewModel(long id);

        Task<bool> DeleteSkill(long id);
    }
}
