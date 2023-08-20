using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.ThingIDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Interfaces
{
    public interface IThingIDoService : IAsyncDisposable
    {
        Task<ThingIDo> GetThingIDOById(long id);

        Task<List<ThingIDoListViewModel>> GetAllThingIDoForIndex();

        Task<bool> CreateOrEditThingIDo(CreateOrEditThingIDoViewModel thingIDo);

        Task<CreateOrEditThingIDoViewModel> FillCreateOrEditThingIDoViewModel(long id);

        Task<bool> DeleteThingIDo(long id);
    }
}
