using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// A specific interface for members
    /// </summary>
    public interface IReadableMembre : IReadable<Membre>
    {
        Task<IList<Membre>> GetBureauAsync();
    }
}