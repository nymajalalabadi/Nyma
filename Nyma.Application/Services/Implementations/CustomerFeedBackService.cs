using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.CustomerFeedBack;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class CustomerFeedBackService : ICustomerFeedBackService
    {
        #region constructor

        private readonly AppDbContext _context;
        public CustomerFeedBackService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<CustomerFeedBack> GetCustomerFeedBackById(long id)
        {
            return await _context.CustomerFeedBacks.SingleOrDefaultAsync(c => c.Id == id);
        }


        public async Task<List<CustomerFeedBackViewModel>> GetCustomerFeedBackForIndex()
        {
            List<CustomerFeedBackViewModel> customerFeedbacks = await _context.CustomerFeedBacks
                .OrderBy(c => c.Order)
                .Select(c => new CustomerFeedBackViewModel()
                {
                    Order = c.Order,
                    Avatar = c.Avatar,
                    Description = c.Description,
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return customerFeedbacks;
        }

        public async Task<CreateOrEditCustomerFeedbackViewModel> FillCreateOrEditCustomerFeedbackViewModel(long id)
        {
            if (id == 0) return new CreateOrEditCustomerFeedbackViewModel()
            {
                Id = 0
            };

            CustomerFeedBack customerFeedBack = await GetCustomerFeedBackById(id);

            if (customerFeedBack == null) return new CreateOrEditCustomerFeedbackViewModel()
            {
                Id = 0
            };

            return new CreateOrEditCustomerFeedbackViewModel()
            {
                Id = customerFeedBack.Id,
                Description = customerFeedBack.Description,
                Avatar = customerFeedBack.Avatar,
                Name = customerFeedBack.Name,
                Order = customerFeedBack.Order,
            };
        }

        public async Task<bool> CreateOrEditCustomerFeedback(CreateOrEditCustomerFeedbackViewModel customerFeedback)
        {
            if (customerFeedback.Id == 0)
            {
                var newcustomerFeedback = new CustomerFeedBack()
                {
                    Name = customerFeedback.Name,
                    Description = customerFeedback.Description,
                    Avatar = customerFeedback.Avatar,
                    Order = customerFeedback.Order
                };

               await _context.CustomerFeedBacks.AddAsync(newcustomerFeedback);
               await _context.SaveChangesAsync();

                return true;
            }

            CustomerFeedBack currentCustomerFeedBack = await GetCustomerFeedBackById(customerFeedback.Id);
            if (currentCustomerFeedBack == null) return false;

            currentCustomerFeedBack.Description = customerFeedback.Description;
            currentCustomerFeedBack.Name = customerFeedback.Name;
            currentCustomerFeedBack.Order = customerFeedback.Order;
            currentCustomerFeedBack.Avatar = customerFeedback.Avatar;

            _context.CustomerFeedBacks.Update(currentCustomerFeedBack);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerFeedback(long id)
        {
            CustomerFeedBack customerFeedback = await GetCustomerFeedBackById(id);

            if (customerFeedback == null) return false;

            _context.CustomerFeedBacks.Remove(customerFeedback);
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
