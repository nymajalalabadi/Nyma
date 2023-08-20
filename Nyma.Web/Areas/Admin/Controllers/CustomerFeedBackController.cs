using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Application.StaticTools;
using Nyma.Domain.ViewModels.CustomerFeedBack;
using Nyma.Application.Eetensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nyma.Application.Generator;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class CustomerFeedBackController : AdminBaseController
    {
        #region Constructor

        private readonly ICustomerFeedBackService _customerFeedBackService;

        public CustomerFeedBackController(ICustomerFeedBackService customerFeedBackService)
        {
            _customerFeedBackService = customerFeedBackService;
        }

        #endregion


        #region list customer feedback

        public async Task<IActionResult> Index()
        {
            return View(await _customerFeedBackService.GetCustomerFeedBackForIndex());
        }

        #endregion


        #region load customer feedback form modal

        public async Task<IActionResult> LoadCustomerfeedbackformModal(long id)
        {
            CreateOrEditCustomerFeedbackViewModel result = await _customerFeedBackService.FillCreateOrEditCustomerFeedbackViewModel(id);

            return PartialView("_CustomerFeedbackModalPartial", result);
        }

        #endregion


        #region submit customer feedback

        public async Task<IActionResult> SubmitCustomerFeedBackFormModal(CreateOrEditCustomerFeedbackViewModel customerFeedback)
        {
            var result = await _customerFeedBackService.CreateOrEditCustomerFeedback(customerFeedback);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete customer feedback

        public async Task<IActionResult> DeleteCustomerFeedBack(long id)
        {
            var result = await _customerFeedBackService.DeleteCustomerFeedback(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region Upload Customer FeedBackImage Ajax

        [HttpPost]
        public async Task<IActionResult> UploadCustomerFeedBackImageAjax(IFormFile file)
        {
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".jpg")
                {
                    var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);

                    await file.AddImageAjaxToServer(imageName, FilePaths.CustomerFeedbackAvatarServer);

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
