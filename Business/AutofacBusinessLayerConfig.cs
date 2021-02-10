using System;
using System.Reflection;
using Autofac;
using AutoMapper;
using Infrastructure;

namespace Business
{
    public class AutofacBusinessLayerConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.QueryObjects")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.Services")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.Facades")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.QueryObjects")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.Services")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Business.Facades")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
