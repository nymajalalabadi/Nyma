using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Eetensions
{
    public static class UploadFileExtension
    {
        public static async Task AddImageAjaxToServer(this IFormFile file, string fileName, string orginalPath)
        {
            if (file != null)
            {
                if (!Directory.Exists(orginalPath))
                {
                    Directory.CreateDirectory(orginalPath);
                }

                string OrginalPath = orginalPath + fileName;

                using (var stream = new FileStream(OrginalPath, FileMode.Create))
                {
                    if (!Directory.Exists(OrginalPath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }
    }
}
