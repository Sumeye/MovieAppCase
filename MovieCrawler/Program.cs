using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MovieApp.Repository.Repositories;
using MovieApp.Repository.UnitOfWork;
using MovieApp.Service.Mapping;
using MovieAppCase.Api.Crawler;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Core.UnitOfWork;
using MovieAppCase.Repository;
using MovieCrawler;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

IHost host = Host.CreateDefaultBuilder(args)
      .ConfigureServices((hostContext, services) =>
      {
          services.AddHostedService<Worker>();
          services.AddTransient<IMovieCrawlerService, MovieCrawlerService>();
          services.AddTransient<IMovieRepository, MoviesRepository>();
          services.AddTransient<IUnitOfWork, UnitOfWork>();
          services.AddDbContext<AppDbContext>(x =>
          {
              x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
              {
                  option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.GetName().Name);
              });
          });
          services.AddAutoMapper(typeof(MapProfile));
      }).Build();

await host.RunAsync();
