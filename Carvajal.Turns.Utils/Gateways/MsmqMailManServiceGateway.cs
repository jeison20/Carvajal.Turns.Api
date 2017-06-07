﻿

using Carvajal.Turns.Utils.Gateways.Interfaces;
using Spring.Messaging.Core;
using System.Configuration;
using System.Messaging;

namespace Carvajal.Turns.Utils.Gateways
{
    public class MsmqMailManServiceGateway : MessageQueueGatewaySupport, IMsmqServiceGateway
    {

        public void Send(Domain.Entities.Message msgMailMan)
        {
            MessageQueue messageQueue = null;
            string path = ConfigurationManager.AppSettings["Path"];
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = msgMailMan.Subject;
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = msgMailMan.Subject;
                }
                messageQueue.Send(msgMailMan.Body);
            }
            catch
            {
                throw;
            }
            finally
            {
                messageQueue.Dispose();
            }
        }
    }
}
