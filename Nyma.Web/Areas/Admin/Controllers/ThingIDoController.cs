using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.ThingIDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class ThingIDoController : AdminBaseController
    {
        #region Constructor

        private readonly IThingIDoService _thingIDoService;

        public ThingIDoController(IThingIDoService thingIDoService)
        {
            _thingIDoService = thingIDoService;
        }

        #endregion


        #region list

        public async Task<IActionResult> Index()
        {
            return View(await _thingIDoService.GetAllThingIDoForIndex());
        }

        #endregion


        #region create or edit thingIDo 

        public async Task<IActionResult> LoadThingIDoFormModal(long id)
        {
            CreateOrEditThingIDoViewModel result = await _thingIDoService.FillCreateOrEditThingIDoViewModel(id);

            return PartialView("_ThingIDoFormModalPartial", result);
        }

        #endregion

        
        #region Submit ThingIDo

        public async Task<IActionResult> SubmitThingIDoFormModal(CreateOrEditThingIDoViewModel thingIDo)
        {
            var result = await _thingIDoService.CreateOrEditThingIDo(thingIDo);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region Delete ThingIDO

        public async Task<IActionResult> DeleteThingIDO(long id)
        {
            var result = await _thingIDoService.DeleteThingIDo(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
