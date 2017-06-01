using Carvajal.Turns.Utils.Gateways.Interfaces;
using Spring.Messaging.Core;

namespace Carvajal.Turns.Utils.Gateways
{
    public class MsmqPurchaseOrderServiceGateway : MessageQueueGatewaySupport, IMsmqPurchaseOrderServiceGateway
    {

        public void Send(PurchaseOrder msgFileMassive)
        {
            // post process message from conversion before sending
            MessageQueueTemplate.ConvertAndSend(msgFileMassive, message => message);
        }
    }
}
