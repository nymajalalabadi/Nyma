using Microsoft.AspNetCore.Mvc;
using Nyma.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyma.Web.Areas.Admin.Controllers
{
    public class MessageController : AdminBaseController
    {
        #region Constructor

        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        #endregion


        #region list of message

        public async Task<IActionResult> Index()
        {
            return View(await _messageService.GetAllMessages());
        }

        #endregion


        #region delete message

        public async Task<IActionResult> DeleteMessage(long id)
        {
            var result = await _messageService.DeleteMessage(id);

            if (result) return new JsonResult(new { status = "Success" });

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
