using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Skill;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        #region constructor

        private readonly AppDbContext _context;

        public SkillService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Skill> GetSkillById(long id)
        {
            return await _context.Skills.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SkillViewModel>> GetAllSkills()
        {
            List<SkillViewModel> skills = await _context.Skills
                .OrderBy(s => s.Order)
                .Select(s => new SkillViewModel()
                {
                    Id = s.Id,
                    Order = s.Order,
                    Percent = s.Percent,
                    Title = s.Title
                }).ToListAsync();

            return skills;
        }

        public async Task<bool> CreateOrEditSkill(CreateOrEditSkillViewModel skill)
        {
            if(skill.Id == 0)
            {
                var newSkill = new Skill()
                {
                    Order = skill.Order,
                    Title = skill.Title,
                    Percent = skill.Percent,
                };

                await _context.Skills.AddAsync(newSkill);
                await _context.SaveChangesAsync();

                return true;
            }

            Skill currentSkill = await GetSkillById(skill.Id);

            if (currentSkill == null) return false;

            currentSkill.Title = skill.Title;
            currentSkill.Order = skill.Order;
            currentSkill.Percent = skill.Percent;

            _context.Skills.Update(currentSkill);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CreateOrEditSkillViewModel> FillCreateOrEditSkillViewModel(long id)
        {
            if (id == 0) return new CreateOrEditSkillViewModel()
            {
                Id = 0
            };

            Skill skill = await GetSkillById(id);

            if (skill == null) return new CreateOrEditSkillViewModel()
            {
                Id = 0
            };

            return new CreateOrEditSkillViewModel()
            {
                Id = skill.Id,
                Percent = skill.Percent,
                Title = skill.Title,
                Order = skill.Order
            };
        }

        public async Task<bool> DeleteSkill(long id)
        {
            Skill skill = await GetSkillById(id);

            if (skill == null) return false;

            _context.Skills.Remove(skill);
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
