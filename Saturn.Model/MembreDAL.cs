using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to members
    /// </summary>
    class MembreDAL : IReadableMembre, ISearchable<Membre>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for members
        /// </summary>
        private readonly MembreReaderClient _proxy = new MembreReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the member corresponding to a code
        /// </summary>
        /// <param name="code">Code of the member desired</param>
        /// <returns>The matching member</returns>
        public Task<Membre> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Membre>();

            _proxy.GetMembreCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembreAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Return all members
        /// </summary>
        /// <returns>All members</returns>
        public Task<IList<Membre>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _proxy.GetMembresCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembresAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetBureauAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _proxy.GetMembresBureauCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembresBureauAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted member's Id
        /// </summary>
        /// <returns>Last inserted member's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetMembreLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembreLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search members
        /// </summary>
        /// <param name="keywords">Keywords separated with a space</param>
        /// <returns>Members matching</returns>
        public Task<IList<Membre>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _proxy.SearchMembresCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchMembresAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}