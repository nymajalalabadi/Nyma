using Nyma.Domain.ViewModels.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Domain.ViewModels.Page
{
    public class PortfolioPageViewModel
    {
        public List<PortfolioViewModel> Portfolios { get; set; }

        public List<PortfolioCategoryViewModel> PortfolioCategories { get; set; }
    }
}
