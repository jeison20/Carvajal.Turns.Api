using Carvajal.Turns.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Carvajal.Turns.Utils.Gateways.Interfaces
{
    public interface IMsmqServiceGateway
    {
        void Send(Message msgMailMan);
    }
}
