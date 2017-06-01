namespace Carvajal.Turns.Utils.Gateways.Interfaces
{
    public interface IMsmqPurchaseOrderServiceGateway
    {
        void Send(PurchaseOrder msgFileMassive);
    }
}
