using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Eetensions;
using Nyma.Application.Generator;
using Nyma.Application.Services.Interfaces;
using Nyma.Application.StaticTools;
using Nyma.Domain.ViewModels.CustomerLogo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class CustomerLogoController : AdminBaseController
    {
        #region Constructor

        private readonly ICustomerLogoService _customerLogoService;

        public CustomerLogoController(ICustomerLogoService customerLogoService)
        {
            _customerLogoService = customerLogoService;
        }

        #endregion


        #region list of customer logo

        public async Task<IActionResult> Index()
        {
            return View(await _customerLogoService.GetCustomerLogoForIndex());
        }

        #endregion

        #region load customer logo form modal

        public async Task<IActionResult> LoadCustomerLogoformModal(long id)
        {
            CreateOrEditCustomerLogoViewModel result = await _customerLogoService.FillCreateOrEditCustomerLogoViewModel(id);

            return PartialView("_CustomerLogoModalPartial", result);
        }

        #endregion


        #region submit customer logo

        public async Task<IActionResult> SubmitCustomerLogoFormModal(CreateOrEditCustomerLogoViewModel customerLogo)
        {
            var result = await _customerLogoService.CreateOrEditCustomerLogo(customerLogo);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete customer logo

        public async Task<IActionResult> DeleteCustomerLogo(long id)
        {
            var result = await _customerLogoService.DeleteCustomerLogo(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region Upload Customer Logo  Ajax

        [HttpPost]
        public async Task<IActionResult> UploadCustomerLogoImageAjax(IFormFile file)
        {
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".jpg")
                {
                    var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    await file.AddImageAjaxToServer(imageName, FilePaths.CustomerLogoServer);

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
