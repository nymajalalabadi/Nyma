using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class SkillController : AdminBaseController
    {
        #region Constructor

        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        #endregion

        #region list

        public async Task<IActionResult> Index()
        {
            return View(await _skillService.GetAllSkills());
        }

        #endregion

        #region load skill form modal

        public async Task<IActionResult> LoadSkillFormModal(long id)
        {
            CreateOrEditSkillViewModel result = await _skillService.FillCreateOrEditSkillViewModel(id);

            return PartialView("_SkillModalPartial", result);
        }

        #endregion


        #region Submit skill

        public async Task<IActionResult> SubmitSkillFormModal(CreateOrEditSkillViewModel skill)
        {
            var result = await _skillService.CreateOrEditSkill(skill);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region Delete skill

        public async Task<IActionResult> DeleteSkill(long id)
        {
            var result = await _skillService.DeleteSkill(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
