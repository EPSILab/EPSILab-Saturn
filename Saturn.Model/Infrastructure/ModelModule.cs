using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;

namespace EPSILab.SolarSystem.Saturn.Model.Infrastructure
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
            builder.RegisterType<MemberDAL>().As<IReadableMember>();
            builder.RegisterType<NewsDAL>().As<IReadableLimitable<News>>();
            builder.RegisterType<ProjectDAL>().As<IReadableLimitable<Project>>();
            builder.RegisterType<ShowDAL>().As<IReadableLimitable<Show>>();

            // Models used by Masters and Details view-models
            builder.RegisterType<ConferenceDAL>().As<IReadable<Conference>>();
            builder.RegisterType<MemberDAL>().As<IReadable<Member>>();
            builder.RegisterType<NewsDAL>().As<IReadable<News>>();
            builder.RegisterType<ProjectDAL>().As<IReadable<Project>>();
            builder.RegisterType<ShowDAL>().As<IReadable<Show>>();

            // Models used by the Search view-model
            builder.RegisterType<NewsDAL>().As<ISearchable<News>>();
            builder.RegisterType<ConferenceDAL>().As<ISearchable<Conference>>();
            builder.RegisterType<ShowDAL>().As<ISearchable<Show>>();

            return builder.Build();
        }
    }
}