using MessageFileMassive = Carvajal.Turns.Utils.Data.MessageFileMassive;

namespace Carvajal.Turns.Utils.Gateways.Interfaces
{
    public interface IMsmqFileMassiveServiceGateway
    {
        void Send(MessageFileMassive msgFileMassive);
    }
}
