

using Carvajal.Turns.Utils.Gateways.Interfaces;
using Spring.Messaging.Core;

namespace Carvajal.Turns.Utils.Gateways
{
    public class MsmqMailManServiceGateway : MessageQueueGatewaySupport, IMsmqServiceGateway
    {

        public void Send(Domain.Entities.Message msgMailMan)
        {
            // post process message from conversion before sending
            MessageQueueTemplate.ConvertAndSend(msgMailMan, message => message);
        }
    }
}
