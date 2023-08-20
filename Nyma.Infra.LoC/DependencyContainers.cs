using Microsoft.Extensions.DependencyInjection;
using Nyma.Application.Services.Implementations;
using Nyma.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Infra.IoC
{
    public class DependencyContainers
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddScoped<IThingIDoService, ThingIDoService>();
            service.AddScoped<ICustomerFeedBackService, CustomerFeedBackService>();
            service.AddScoped<ICustomerLogoService, CustomerLogoService>();
            service.AddScoped<IEducationService, EducationService>();
            service.AddScoped<IExperienceService, ExperienceService>();
            service.AddScoped<ISkillService, SkillService>();
            service.AddScoped<IPorfolioService, PorfolioService>();
            service.AddScoped<ISocialMediaService, SocialMediaService>();
            service.AddScoped<IInformationService, InformationService>();
            service.AddScoped<IMessageService, MessageService>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
