using Nyma.Domain.ViewModels.CustomerFeedBack;
using Nyma.Domain.ViewModels.CustomerLogo;
using Nyma.Domain.ViewModels.ThingIDo;
using System.Collections.Generic;


namespace Nyma.Domain.ViewModels.Page
{
    public class IndexPageViewModel
    {
        public List<ThingIDoListViewModel> ThingIDoList { get; set; }
        public List<CustomerFeedBackViewModel> CustomerFeedBackList { get; set; }
        public List<CustomerLogoViewModel> CustomerLogoList { get; set; }
    }
}
