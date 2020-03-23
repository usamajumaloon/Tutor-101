using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutor101.Service.BO.Security;
using Tutor101.ViewModels.Security;

namespace Tutor101.Initializers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //User
            CreateMap<LoginViewModel, LoginBO>().ReverseMap();
        }
    }
}
