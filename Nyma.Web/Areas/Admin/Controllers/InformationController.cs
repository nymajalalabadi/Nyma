using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Eetensions;
using Nyma.Application.Generator;
using Nyma.Application.Services.Interfaces;
using Nyma.Application.StaticTools;
using Nyma.Domain.ViewModels.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class InformationController : AdminBaseController
    {
        #region Constructor

        public readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }

        #endregion


        #region information 

        public async Task<IActionResult> LoadInformationFormModal()
        {
            CreateOrEditInformationViewModel result = await _informationService.FillCreateOrEditInformationViewModel();

            return PartialView("_InformationFormModalPartial", result);
        }

        #endregion

        #region submit information form modal

        public async Task<IActionResult> SubmitInformationFormModal(CreateOrEditInformationViewModel information)
        {
            var result = await _informationService.CreateOrEditInformation(information);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region upload information avatar ajax

        [HttpPost]
        public async Task<IActionResult> UploadInformationAvatarAjax(IFormFile file)
        {
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".jpg")
                {
                    var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    await file.AddImageAjaxToServer(imageName, FilePaths.AvatarServer);
                    return new JsonResult(new { status = "Success", imageName = imageName });

                }
                else
                {
                    return new JsonResult(new { status = "Error" });
                }
            }
            else
            {
                return new JsonResult(new { status = "Error" });
            }
        }

        #endregion


        #region upload information resume ajax

        [HttpPost]
        public async Task<IActionResult> UploadInformationResumeAjax(IFormFile file)
        {
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) == ".pdf")
                {
                    var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    await file.AddImageAjaxToServer(imageName, FilePaths.ResumeServer);
                    return new JsonResult(new { status = "Success", imageName = imageName });

                }
                else
                {
                    return new JsonResult(new { status = "Error" });
                }
            }
            else
            {
                return new JsonResult(new { status = "Error" });
            }
        }

        #endregion
    }
}
