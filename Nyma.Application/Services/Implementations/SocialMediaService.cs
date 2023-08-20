using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.ViewModels.SocialMedia;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class SocialMediaService : ISocialMediaService
    {
        #region constructor

        private readonly AppDbContext _context;

        public SocialMediaService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<List<SocialMediaViewModel>> GetAllSocialMedias()
        {
            List<SocialMediaViewModel> socialMedias = await _context.SocialMedias
                .OrderBy(s => s.Order)
                .Select(s => new SocialMediaViewModel()
                {
                    Icon = s.Icon,
                    Link = s.Link,
                    Id = s.Id,
                    Order = s.Order
                })
                .ToListAsync();

            return socialMedias;
        }

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion
    }
}
