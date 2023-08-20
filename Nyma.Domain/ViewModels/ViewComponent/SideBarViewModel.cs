using Nyma.Domain.ViewModels.Information;
using Nyma.Domain.ViewModels.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Domain.ViewModels.ViewComponent
{
    public class SideBarViewModel
    {
        public List<SocialMediaViewModel> SocialMedias { get; set; }

        public InformationViewModel Information { get; set; }

    }
}
