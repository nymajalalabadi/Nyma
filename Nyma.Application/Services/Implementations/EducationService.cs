using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Education;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class EducationService : IEducationService
    {
        #region constructor

        private readonly AppDbContext _context;

        public EducationService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Education> GetEducationById(long id)
        {
            return await _context.Educations.SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<List<EducationViewModel>> GetAllEducations()
        {
            List<EducationViewModel> educations = await _context.Educations
                .OrderBy(e => e.Order)
                .Select(e => new EducationViewModel() 
                {
                    Description = e.Description,
                    EndDate = e.EndDate,
                    Id = e.Id,
                    StartDate = e.StartDate,
                    Title = e.Title,
                    Order = e.Order
                }).ToListAsync();

            return educations;
        }

        public async Task<bool> CreateOrEditEducation(CreateOrEditEducationViewModel education)
        {
            if (education.Id == 0)
            {
                var newEducation = new Education()
                {
                    StartDate = education.StartDate,
                    EndDate = education.EndDate,
                    Description = education.Description,
                    Title = education.Title,
                    Order = education.Order,
                };

                await _context.Educations.AddAsync(newEducation);
                await _context.SaveChangesAsync();

                return true;
            }

            Education currentEducation = await GetEducationById(education.Id);

            if (currentEducation == null) return false;

            currentEducation.Title = education.Title;
            currentEducation.Order = education.Order;
            currentEducation.StartDate = education.StartDate;
            currentEducation.EndDate = education.EndDate;
            currentEducation.Description = education.Description;

            _context.Educations.Update(currentEducation);
           await _context.SaveChangesAsync();

            return true;
        }


        public async Task<CreateOrEditEducationViewModel> FillCreateOrEditEducationViewModel(long id)
        {
            if (id == 0) return new CreateOrEditEducationViewModel()
            {
                Id = 0,
            };

            Education education = await GetEducationById(id);

            if (education == null) return new CreateOrEditEducationViewModel()
            {
                Id = 0
            };

            return new CreateOrEditEducationViewModel()
            {
                Id = education.Id,
                Description = education.Description,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                Order = education.Order,
                Title = education.Title
            };
        }

        public async Task<bool> DeleteEducation(long id)
        {
            Education education = await GetEducationById(id);

            if (education == null) return false;

            _context.Educations.Remove(education);
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
