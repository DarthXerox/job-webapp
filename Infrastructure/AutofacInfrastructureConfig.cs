using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Infrastructure
{
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "Infrastructure.Queries")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<DAL.JobDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
