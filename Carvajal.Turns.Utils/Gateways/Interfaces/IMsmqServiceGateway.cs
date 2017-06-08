using Carvajal.Turns.Domain.Entities;


namespace Carvajal.Turns.Utils.Gateways.Interfaces
{
    /// <summary>
    /// Metodo utilizado para el envio de correos
    /// </summary>
    /// <param name="msgMailMan">Objeto tipo Message que contiene los parametros necesarios para el envio del correo </param>
    public interface IMsmqServiceGateway
    {
        void Send(Message msgMailMan);
    }
}
