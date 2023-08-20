using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Experience;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class ExperienceService : IExperienceService
    {
        #region constructor

        private readonly AppDbContext _context;

        public ExperienceService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Experience> GetExperienceById(long Id)
        {
            return await _context.Experiences.SingleOrDefaultAsync(e => e.Id == Id);
        }


        public async Task<List<ExperienceViewModel>> GetAllExperiences()
        {
            List<ExperienceViewModel> educations = await _context.Experiences
                    .OrderBy(c => c.Order)
                    .Select(c => new ExperienceViewModel()
                    {
                        Id = c.Id,
                        Description = c.Description,
                        EndDate = c.EndDate,
                        StartDate = c.StartDate,
                        Title = c.Title,
                        Order = c.Order
                    })
                    .ToListAsync();

            return educations;
        }

        public async Task<bool> CreateOrEditExperience(CreateOrEditExperienceViewModel experience)
        {
            if (experience.Id == 0)
            {
                var newExperience = new Experience()
                {
                    Description = experience.Description,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    Order = experience.Order,
                    Title = experience.Title
                };

                await _context.Experiences.AddAsync(newExperience);
                await _context.SaveChangesAsync();

                return true;
            }

            Experience currentExperience = await GetExperienceById(experience.Id);

            if (currentExperience == null) return false;

            currentExperience.Description = experience.Description;
            currentExperience.EndDate = experience.EndDate;
            currentExperience.Order = experience.Order;
            currentExperience.StartDate = experience.StartDate;
            currentExperience.Title = experience.Title;

            _context.Experiences.Update(currentExperience);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CreateOrEditExperienceViewModel> FillCreateOrEditExperienceViewModel(long id)
        {
            if (id == 0) return new CreateOrEditExperienceViewModel() { Id = 0 };

            Experience experience = await GetExperienceById(id);

            if (experience == null) return new CreateOrEditExperienceViewModel() { Id = 0 };

            return new CreateOrEditExperienceViewModel()
            {
                Id = experience.Id,
                Description = experience.Description,
                EndDate = experience.EndDate,
                Order = experience.Order,
                StartDate = experience.StartDate,
                Title = experience.Title
            };
        }

        public async Task<bool> DeleteExperience(long id)
        {
            Experience experience = await GetExperienceById(id);

            if (experience == null) return false;

            _context.Experiences.Remove(experience);
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
