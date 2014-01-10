using Autofac;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model.Infrastructure
{
    /// <summary>
    /// Build links between model interfaces and model classes with Autofac
    /// </summary>
    public class ModelBuilder
    {
        public IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Models used by the Main view-model
            builder.RegisterType<ConferenceDAL>().As<IReadableLimitable<Conference>>();
            builder.RegisterType<MembreDAL>().As<IReadableMembre>();
            builder.RegisterType<NewsDAL>().As<IReadableLimitable<News>>();
            builder.RegisterType<ProjetDAL>().As<IReadableLimitable<Projet>>();
            builder.RegisterType<SalonDAL>().As<IReadableLimitable<Salon>>();

            // Models used by Masters and Details view-models
            builder.RegisterType<ConferenceDAL>().As<IReadable<Conference>>();
            builder.RegisterType<MembreDAL>().As<IReadable<Membre>>();
            builder.RegisterType<NewsDAL>().As<IReadable<News>>();
            builder.RegisterType<ProjetDAL>().As<IReadable<Projet>>();
            builder.RegisterType<SalonDAL>().As<IReadable<Salon>>();

            // Models used by the Search view-model
            builder.RegisterType<NewsDAL>().As<ISearchable<News>>();
            builder.RegisterType<ConferenceDAL>().As<ISearchable<Conference>>();
            builder.RegisterType<SalonDAL>().As<ISearchable<Salon>>();

            return builder.Build();
        }
    }
}