using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to the projects
    /// </summary>
    public class ProjetDAL : IReadableWithFilter<Projet, Ville>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for projects
        /// </summary>
        private readonly ProjetReaderClient _proxy = new ProjetReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the project corresponding to a code
        /// </summary>
        /// <param name="code">Code of the project desired</param>
        /// <returns>The matching project</returns>
        public Task<Projet> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Projet>();

            _proxy.GetProjetCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all projects
        /// </summary>
        /// <returns>All projects</returns>
        public Task<IList<Projet>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _proxy.GetProjetsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetsAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of projects which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>List limited of projects</returns>
        public Task<IList<Projet>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _proxy.GetProjetsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetsLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all projects of a campus
        /// </summary>
        /// <param name="ville">The campus</param>
        /// <returns>All campus's projects</returns>
        public Task<IList<Projet>> GetAsync(Ville ville)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _proxy.GetProjetsByVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetsByVilleAsync(ville);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of projects of a campus which can be limited
        /// </summary>
        /// <param name="ville">The campus</param>
        /// /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfElements">Number of results desired</param>
        /// <returns>All campus's projects</returns>
        public Task<IList<Projet>> GetAsync(Ville ville, int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _proxy.GetProjetsByVilleLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetsByVilleLimitedAsync(ville, indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted project's Id
        /// </summary>
        /// <returns>Last inserted project's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetProjetLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjetLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        #endregion
    }
}