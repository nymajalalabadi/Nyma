using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Controllers
{
    public class ResumeController : Controller
    {
        #region Constructor

        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;
        private readonly ISkillService _skillService;

        public ResumeController(IEducationService educationService, IExperienceService experienceService, ISkillService skillService)
        {
            _educationService = educationService;
            _experienceService = experienceService;
            _skillService = skillService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            ResumePageViewModel resume = new ResumePageViewModel()
            {
                Educations = await _educationService.GetAllEducations(),
                experiences = await _experienceService.GetAllExperiences(),
                skills = await _skillService.GetAllSkills()
            };

            return View(resume);
        }
    }
}
