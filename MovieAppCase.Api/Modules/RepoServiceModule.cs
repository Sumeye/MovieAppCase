using Autofac;
using MovieApp.Repository.Repositories;
using MovieApp.Repository.UnitOfWork;
using MovieApp.Service.Mapping;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Core.Services;
using MovieAppCase.Core.UnitOfWork;
using MovieAppCase.Repository;
using MovieAppCase.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace MovieApp.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
           .As(typeof(IGenericRepository<>))
           .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>))
                   .As(typeof(IService<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
