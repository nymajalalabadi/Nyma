using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Implementations;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Controllers
{
    public class ContactController : Controller
    {
        #region Constructor

        private readonly IMessageService _messageService;

        private readonly ICaptchaValidator _captchaValidator;

        private readonly IInformationService _informationService;

        public ContactController(IMessageService messageService, ICaptchaValidator captchaValidator, IInformationService informationService)
        {
            _messageService = messageService;
            _captchaValidator = captchaValidator;
            _informationService = informationService;
        }

        #endregion

        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateMessageViewModel message)
        {
            ViewData["Information"] = await _informationService.GetAllInformation();

            if (!await _captchaValidator.IsCaptchaPassedAsync(message.Captcha))
            {
                ViewData["FormSubmitResult"] = false;
                return View(message);
            }

            if (!ModelState.IsValid)
            {
                return View(message);
            }

            var result = await _messageService.CreateMessage(message);

            if (result)
            {
                ViewData["FormSubmitResult"] = true;
            }

            return View();
        }

    }
}
