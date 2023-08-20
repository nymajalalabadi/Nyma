using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class PortfolioCategoryController : AdminBaseController
    {
        #region Constructor

        private readonly IPorfolioService _porfolioService;

        public PortfolioCategoryController(IPorfolioService porfolioService)
        {
            _porfolioService = porfolioService;
        }

        #endregion


        #region list of portfolio category

        public async Task<IActionResult> Index()
        {
            return View(await _porfolioService.GetAllPortfolioCategories());
        }

        #endregion


        #region load portfolio category form modal

        public async Task<IActionResult> LoadPortfolioCategoryformModal(long id)
        {
            CreateOrEditPortfolioCategoryViewModel result = await _porfolioService.FillCreateOrEditPortfolioCategoryViewModel(id);

            return PartialView("_PortfolioCategoryModalPartial", result);
        }

        #endregion


        #region submit portfolio category form modal

        public async Task<IActionResult> SubmitPortfolioCategoryFormModal(CreateOrEditPortfolioCategoryViewModel portfolioCategory)
        {
            var result = await _porfolioService.CreateOrEditPortfolioCategory(portfolioCategory);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion


        #region delete portfolio category

        public async Task<IActionResult> DeletePortfolioCategory(long id)
        {
            var result = await _porfolioService.DeletePortfolioCategory(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
