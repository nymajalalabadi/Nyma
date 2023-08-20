using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IPorfolioService : IAsyncDisposable
    {
        #region Portfolio

        Task<Portfolio> GetPortfolioById(long id);

        Task<List<PortfolioViewModel>> GetAllPortfolios();

        Task<CreateOrEditPortfolioViewModel> FillCreateOrEditPortfolioViewModel(long id);

        Task<bool> CreateOrEditPortfolio(CreateOrEditPortfolioViewModel portfolio);

        Task<bool> DeletePortfolio(long id);

        #endregion


        #region Portfolio Categories

        Task<PortfolioCategory> GetPortfolioCategoryById(long id);

        Task<List<PortfolioCategoryViewModel>> GetAllPortfolioCategories();
        
        Task<bool> CreateOrEditPortfolioCategory(CreateOrEditPortfolioCategoryViewModel portfolioCategory);

        Task<CreateOrEditPortfolioCategoryViewModel> FillCreateOrEditPortfolioCategoryViewModel(long id);

        Task<bool> DeletePortfolioCategory(long id);

        #endregion


    }
}
