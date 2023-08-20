using Microsoft.EntityFrameworkCore;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.ThingIDo;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class ThingIDoService: IThingIDoService
    {
        #region constructor

        private readonly AppDbContext _context;

        public ThingIDoService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<ThingIDo> GetThingIDOById(long id)
        {
            return await _context.ThingIDos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<ThingIDoListViewModel>> GetAllThingIDoForIndex()
        {
            List<ThingIDoListViewModel> thingIDos = await _context.ThingIDos
                .OrderBy(t => t.Order)
                .Select(t => new ThingIDoListViewModel()
                {
                    Id = t.Id,
                    ColumnLg = t.ColumnLg,
                    Description = t.Description,
                    Title = t.Title,
                    Icon = t.Icon
                }).ToListAsync();

            return thingIDos;
        }

        public async Task<bool> CreateOrEditThingIDo(CreateOrEditThingIDoViewModel thingIDo)
        {
            if (thingIDo.Id == 0)
            {
                var newThingIDo = new ThingIDo()
                {
                    Description = thingIDo.Description,
                    Title = thingIDo.Title,
                    ColumnLg = thingIDo.ColumnLg,
                    Icon = thingIDo.Icon,
                    Order = thingIDo.Order
                };

               await _context.ThingIDos.AddAsync(newThingIDo);
               await _context.SaveChangesAsync();

                return true;
            }

            ThingIDo currentThingIDo = await GetThingIDOById(thingIDo.Id);

            if (currentThingIDo == null) return false;

            currentThingIDo.Description = thingIDo.Description;
            currentThingIDo.Title = thingIDo.Title;
            currentThingIDo.ColumnLg = thingIDo.ColumnLg;
            currentThingIDo.Icon = thingIDo.Icon;
            currentThingIDo.Order = thingIDo.Order;

            _context.ThingIDos.Update(currentThingIDo); 
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CreateOrEditThingIDoViewModel> FillCreateOrEditThingIDoViewModel(long id)
        {
            if (id == 0) return new CreateOrEditThingIDoViewModel() 
            { 
                Id = 0
            };

            ThingIDo thingIDo = await GetThingIDOById(id);

            if (thingIDo == null) return new CreateOrEditThingIDoViewModel() 
            { 
                Id = 0 
            };

            return new CreateOrEditThingIDoViewModel()
            {
                Id = thingIDo.Id,
                ColumnLg = thingIDo.ColumnLg,
                Description = thingIDo.Description,
                Icon = thingIDo.Icon,
                Order = thingIDo.Order,
                Title = thingIDo.Title
            };
        }

        public async Task<bool> DeleteThingIDo(long id)
        {
            ThingIDo thingIDo = await GetThingIDOById(id);

            if (thingIDo == null) return false;

            _context.ThingIDos.Remove(thingIDo);
            await _context.SaveChangesAsync();

            return true;
        }

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion

    }
}
