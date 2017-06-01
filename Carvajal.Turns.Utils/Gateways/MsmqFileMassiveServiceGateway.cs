
using Carvajal.Turns.Utils.Gateways.Interfaces;
using Spring.Messaging.Core;
using MessageFileMassive = Carvajal.Turns.Utils.Data.MessageFileMassive;

namespace Carvajal.Turns.Utils.Gateways
{
    class MsmqFileMassiveServiceGateway : MessageQueueGatewaySupport, IMsmqFileMassiveServiceGateway
    {

        public void Send(MessageFileMassive msgFileMassive)
        {
            // post process message from conversion before sending
            MessageQueueTemplate.ConvertAndSend(msgFileMassive, message => message);
        }
    }
}
