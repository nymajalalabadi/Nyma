using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.ViewComponents
{
    public class MetaTagViewComponent : ViewComponent
    {
        #region MyRegion
        private readonly IInformationService _informationService;

        public MetaTagViewComponent(IInformationService informationService)
        {
            _informationService = informationService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _informationService.GetAllInformation();

            return View("MetaTag", result);
        }
    }
}
