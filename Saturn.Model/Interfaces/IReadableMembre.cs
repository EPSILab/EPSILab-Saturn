using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// A specific interface for members
    /// </summary>
    public interface IReadableMembre : IReadable<Membre>
    {
        /// <summary>
        /// Returns all members of the bureau
        /// </summary>
        /// <returns>List of the members of the bureau</returns>
        Task<IList<Membre>> GetBureauAsync();
    }
}