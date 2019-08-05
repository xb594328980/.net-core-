using AutoMapper;
using Sansunt.MicroService.Demo.Application.ViewModels;
using Sansunt.MicroService.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Staff, StaffViewModel>();
        }
    }
}
