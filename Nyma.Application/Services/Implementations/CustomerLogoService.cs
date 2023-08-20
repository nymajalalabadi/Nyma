using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.CustomerLogo;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class CustomerLogoService : ICustomerLogoService
    {
        #region constructor

        private readonly AppDbContext _context;

        public CustomerLogoService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<CustomerLogo> GetCustomerLogoById(long id)
        {
            return await _context.CustomerLogos.SingleOrDefaultAsync(c => c.Id == id);
        }


        public async Task<List<CustomerLogoViewModel>> GetCustomerLogoForIndex()
        {
            List<CustomerLogoViewModel> CustomerLogo = await _context.CustomerLogos
                .OrderBy(c => c.Order)
                .Select(c => new CustomerLogoViewModel()
                {
                    Id = c.Id,
                    Logo = c.Logo,
                    LogoAlt = c.LogoAlt,
                    Link = c.Link,
                    Order = c.Order
                }).ToListAsync();

            return CustomerLogo;
        }

        public async Task<CreateOrEditCustomerLogoViewModel> FillCreateOrEditCustomerLogoViewModel(long id)
        {
            if (id == 0) return new CreateOrEditCustomerLogoViewModel()
            {
                Id = 0
            };

            CustomerLogo customerLogo = await GetCustomerLogoById(id);

            if (customerLogo == null) return new CreateOrEditCustomerLogoViewModel()
            {
                Id = 0
            };

            return new CreateOrEditCustomerLogoViewModel()
            {
                Id = customerLogo.Id,
                Link = customerLogo.Link,
                Logo = customerLogo.Logo,
                LogoAlt = customerLogo.LogoAlt,
                Order = customerLogo.Order
            };
        }

        public async Task<bool> CreateOrEditCustomerLogo(CreateOrEditCustomerLogoViewModel customerLogo)
        {
            if(customerLogo.Id == 0)
            {
                var newCustomerLogo = new CustomerLogo()
                {
                    Link = customerLogo.Link,
                    Logo = customerLogo.Logo,
                    LogoAlt = customerLogo.LogoAlt,
                    Order = customerLogo.Order
                };

                await _context.CustomerLogos.AddAsync(newCustomerLogo);
                await _context.SaveChangesAsync();

                return true;
            }

            CustomerLogo currentCustomerLogo = await GetCustomerLogoById(customerLogo.Id);

            if (currentCustomerLogo == null) return false;

            currentCustomerLogo.Link = customerLogo.Link;
            currentCustomerLogo.Logo = customerLogo.Logo;
            currentCustomerLogo.LogoAlt = customerLogo.LogoAlt;
            currentCustomerLogo.Order = customerLogo.Order;

             _context.CustomerLogos.Update(currentCustomerLogo);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerLogo(long id)
        {
            CustomerLogo customerLogo = await GetCustomerLogoById(id);

            if (customerLogo == null) return false;

            _context.CustomerLogos.Remove(customerLogo);
            await _context.SaveChangesAsync();

            return true;
        }

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion
    }
}
