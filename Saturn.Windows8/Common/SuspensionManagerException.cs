using System;

namespace EPSILab.SolarSystem.Saturn.Windows8.Common
{
    public class SuspensionManagerException : Exception
    {
        public SuspensionManagerException()
        {
        }

        public SuspensionManagerException(Exception e) : base("SuspensionManager failed", e)
        {
        }
    }
}