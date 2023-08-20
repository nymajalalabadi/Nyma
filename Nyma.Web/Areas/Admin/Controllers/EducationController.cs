using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class EducationController : AdminBaseController
    {
        #region Constructor

        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        #endregion


        #region list education

        public async Task<IActionResult> Index()
        {
            return View(await _educationService.GetAllEducations());
        }

        #endregion


        #region load education form modal

        public async Task<IActionResult> LoadEducationFormModal(long id)
        {
            CreateOrEditEducationViewModel result = await _educationService.FillCreateOrEditEducationViewModel(id);

            return PartialView("_EducationModalPartial", result);
        }

        #endregion


        #region submit education

        public async Task<IActionResult> SubmitEducationFormModal(CreateOrEditEducationViewModel education)
        {
            var result = await _educationService.CreateOrEditEducation(education);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete education

        public async Task<IActionResult> DeleteEducation(long id)
        {
            var result = await _educationService.DeleteEducation(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion

    }
}
