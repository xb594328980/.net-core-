using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration;

namespace Sansunt.MicroService.Demo.Infra.Mapper
{
    public class AutoMapperConfig
    {
        public static IMapper InstanceMapping()
        {

            //创建AutoMapperConfiguration, 提供静态方法Configure，一次加载所有层中Profile定义 
            //MapperConfiguration实例可以静态存储在一个静态字段中，也可以存储在一个依赖注入容器中。 一旦创建，不能更改/修改。
            return new MapperConfiguration(cfg =>
            {
                //这个是领域模型 -> 视图模型的映射，是 读命令
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                //这里是视图模型 -> 领域模式的映射，是 写 命令
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
                //请求模型转命令模型
                cfg.AddProfile(new RequestModelToCommandModel());
                //命令转实体
                cfg.AddProfile(new CommandModelToEntity());
            }).CreateMapper();
        }

        public static IMapper StaticMapping()
        {
            MapperConfigurationExpression mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfiles(Profiles);
            AutoMapper.Mapper.Initialize(mapperConfigurationExpression);
            return AutoMapper.Mapper.Instance;
        }


        private static Profile[] Profiles
        {
            get
            {
                return new Profile[]
                {
                    new DomainToViewModelMappingProfile(),//这个是领域模型 -> 视图模型的映射，是 读命令
                    new ViewModelToDomainMappingProfile(), //这里是视图模型 -> 领域模式的映射，是 写 命令
                    new RequestModelToCommandModel(), //请求模型转命令模型
                    new CommandModelToEntity()  //命令转实体
                };
            }
        }
    }
}
