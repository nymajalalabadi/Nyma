using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.ViewComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        #region Constructor

        private readonly ISocialMediaService _socialMediaService;

        private readonly IInformationService _informationService;

        public SideBarViewComponent(ISocialMediaService socialMediaService, IInformationService informationService)
        {
            _socialMediaService = socialMediaService;
            _informationService = informationService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SideBarViewModel sideBar = new SideBarViewModel()
            {
                SocialMedias = await _socialMediaService.GetAllSocialMedias(),
                Information = await _informationService.GetAllInformation(),
            };

            return View("SideBar" , sideBar);
        }
    }
}
