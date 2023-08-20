using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class ExperienceController : AdminBaseController
    {
        #region Constructor

        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        #endregion


        #region list experience

        public async Task<IActionResult> Index()
        {
            return View( await _experienceService.GetAllExperiences());
        }

        #endregion


        #region load experience formModal

        public async Task<IActionResult> LoadExperienceFormModal(long id)
        {
            CreateOrEditExperienceViewModel result = await _experienceService.FillCreateOrEditExperienceViewModel(id);

            return PartialView("_ExperienceModalPartial", result);
        }

        #endregion


        #region submit experience

        public async Task<IActionResult> SubmitExperienceFormModal(CreateOrEditExperienceViewModel experience)
        {
            var result = await _experienceService.CreateOrEditExperience(experience);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete experience

        public async Task<IActionResult> DeleteExperience(long id)
        {
            var result = await _experienceService.DeleteExperience(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
