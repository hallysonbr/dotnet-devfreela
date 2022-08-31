using System;

namespace DevFreela.Core.Exceptions
{
    public class ProjectAlreadyStarted : Exception
    {
        public ProjectAlreadyStarted() : base("Project is already in Started status")
        {
        }
    }
}
