using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System;
using System.Collections.Generic;

namespace SolarSystem.Saturn.Model.Factory
{
    public class ModelFactory<T>
    {
        private static readonly IDictionary<Type, Func<IModel<T>>> _models = new Dictionary<Type, Func<IModel<T>>>
            {
                { typeof(Conference), () => new ConferenceModel() as IModel<T>},
                { typeof(Membre), () => new MembreModel() as IModel<T>},
                { typeof(News), () => new NewsModel() as IModel<T>},
                { typeof(Projet), () => new ProjetModel() as IModel<T>},
                { typeof(Role), () => new RoleModel() as IModel<T>},
                { typeof(Salon), () => new SalonModel() as IModel<T>},
                { typeof(Ville), () => new VilleModel() as IModel<T>},
            };

        public static IModel<T> CreateModel()
        {
            return _models[typeof(T)]();
        }
    }
}
