using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Controllers
{
    public class PortfolioController : Controller
    {
        #region Constructor

        private readonly IPorfolioService _porfolioService;

        public PortfolioController(IPorfolioService porfolioService)
        {
            _porfolioService = porfolioService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            PortfolioPageViewModel index = new PortfolioPageViewModel()
            {
                Portfolios = await _porfolioService.GetAllPortfolios(),
                PortfolioCategories = await _porfolioService.GetAllPortfolioCategories()
            };

            return View(index);
        }
    }
}
