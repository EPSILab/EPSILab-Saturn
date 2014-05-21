using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// A specific interface for members
    /// </summary>
    public interface IReadableMember : IReadable<Member>
    {
        Task<IList<Member>> GetBureauAsync();
    }
}