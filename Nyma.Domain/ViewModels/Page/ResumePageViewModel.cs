using Nyma.Domain.ViewModels.Education;
using Nyma.Domain.ViewModels.Experience;
using Nyma.Domain.ViewModels.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Domain.ViewModels.Page
{
    public class ResumePageViewModel
    {
        public List<EducationViewModel> Educations { get; set; }

        public List<ExperienceViewModel> experiences { get; set; }

        public List<SkillViewModel> skills{ get; set; }
    }
}
