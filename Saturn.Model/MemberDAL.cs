using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to members
    /// </summary>
    class MemberDAL : IReadableMember, ISearchable<Member>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for members
        /// </summary>
        private readonly MemberReaderClient _proxy = new MemberReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the member corresponding to a code
        /// </summary>
        /// <param name="code">Code of the member desired</param>
        /// <returns>The matching member</returns>
        public Task<Member> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Member>();

            _proxy.GetMemberCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMemberAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Return all members
        /// </summary>
        /// <returns>All members</returns>
        public Task<IList<Member>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Member>>();

            _proxy.GetMembersCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembersAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Member>> GetBureauAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Member>>();

            _proxy.GetMembersBureauCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMembersBureauAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted member's Id
        /// </summary>
        /// <returns>Last inserted member's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetMemberLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetMemberLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search members
        /// </summary>
        /// <param name="keywords">Keywords separated with a space</param>
        /// <returns>Members matching</returns>
        public Task<IList<Member>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Member>>();

            _proxy.SearchMembersCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchMembersAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}