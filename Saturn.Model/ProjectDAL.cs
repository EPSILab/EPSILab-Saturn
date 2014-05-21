using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to the projects
    /// </summary>
    class ProjectDAL : IReadableWithFilter<Project, Campus>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for projects
        /// </summary>
        private readonly ProjectReaderClient _proxy = new ProjectReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the project corresponding to a code
        /// </summary>
        /// <param name="code">Code of the project desired</param>
        /// <returns>The matching project</returns>
        public Task<Project> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Project>();

            _proxy.GetProjectCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all projects
        /// </summary>
        /// <returns>All projects</returns>
        public Task<IList<Project>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Project>>();

            _proxy.GetProjectsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectsAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of projects which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>List limited of projects</returns>
        public Task<IList<Project>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Project>>();

            _proxy.GetProjectsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectsLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all projects of a campus
        /// </summary>
        /// <param name="ville">The campus</param>
        /// <returns>All campus's projects</returns>
        public Task<IList<Project>> GetAsync(Campus ville)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Project>>();

            _proxy.GetProjectsByCampusCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectsByCampusAsync(ville);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of projects of a campus which can be limited
        /// </summary>
        /// <param name="ville">The campus</param>
        /// /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfElements">Number of results desired</param>
        /// <returns>All campus's projects</returns>
        public Task<IList<Project>> GetAsync(Campus ville, int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Project>>();

            _proxy.GetProjectsByCampusLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectsByCampusLimitedAsync(ville, indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted project's Id
        /// </summary>
        /// <returns>Last inserted project's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetProjectLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetProjectLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        #endregion
    }
}