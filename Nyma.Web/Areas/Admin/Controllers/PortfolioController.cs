using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Eetensions;
using Nyma.Application.Generator;
using Nyma.Application.Services.Interfaces;
using Nyma.Application.StaticTools;
using Nyma.Domain.ViewModels.Portfolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class PortfolioController : AdminBaseController
    {
        #region Constructor

        private readonly IPorfolioService _porfolioService;

        public PortfolioController(IPorfolioService porfolioService)
        {
            _porfolioService = porfolioService;
        }

        #endregion


        #region Porfolio

        public async Task<IActionResult> Index()
        {
            return View(await _porfolioService.GetAllPortfolios());
        }

        #endregion


        #region load portfolio  form modal

        public async Task<IActionResult> LoadPortfolioformModal(long id)
        {
            CreateOrEditPortfolioViewModel result = await _porfolioService.FillCreateOrEditPortfolioViewModel(id);

            return PartialView("_PortfolioModalPartial", result);
        }

        #endregion


        #region submit portfolio  form modal

        public async Task<IActionResult> SubmitPortfolioFormModal(CreateOrEditPortfolioViewModel portfolio)
        {
            var result = await _porfolioService.CreateOrEditPortfolio(portfolio);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete portfolio 

        public async Task<IActionResult> DeletePortfolio(long id)
        {
            var result = await _porfolioService.DeletePortfolio(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region Upload portfolio  Ajax

        [HttpPost]
        public async Task<IActionResult> UploadPortfolioImageAjax(IFormFile file)
        {
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".jpg")
                {
                    var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    await file.AddImageAjaxToServer(imageName, FilePaths.PortfolioServer);

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
