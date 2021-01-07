using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
                .Where(t => t.Namespace == nameof(QueryObjects))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == nameof(Services))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == nameof(Facades))
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
                .Where(t => t.Namespace == nameof(QueryObjects))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == nameof(Services))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == nameof(Facades))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
