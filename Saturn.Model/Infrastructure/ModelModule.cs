using Autofac;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model.Infrastructure
{
    public class ModelBuilder
    {
        public IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ConferenceDAL>().As<IReadableLimitable<Conference>>();
            builder.RegisterType<MembreDAL>().As<IReadableMembre>();
            builder.RegisterType<NewsDAL>().As<IReadableLimitable<News>>();
            builder.RegisterType<ProjetDAL>().As<IReadableLimitable<Projet>>();
            builder.RegisterType<SalonDAL>().As<IReadableLimitable<Salon>>();

            builder.RegisterType<NewsDAL>().As<ISearchable<News>>();
            builder.RegisterType<ConferenceDAL>().As<ISearchable<Conference>>();
            builder.RegisterType<SalonDAL>().As<ISearchable<Salon>>();

            return builder.Build();
        }
    }
}