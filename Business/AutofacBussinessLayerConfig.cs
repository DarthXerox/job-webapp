using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using Infrastructure;

namespace Business
{
    public class AutofacBussinessLayerConfig : Autofac.Module
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Bussiness.QueryObjects")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Bussiness.Services")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Bussiness.Facades")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
