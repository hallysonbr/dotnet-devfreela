using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.CrossCutting.IntegrationEvents
{
    public class PaymentApprovedIntegrationEvent
    {
        public PaymentApprovedIntegrationEvent(int idProject)
        {
            IdProject = idProject;
        }

        public int IdProject { get; set; }
    }
}
