namespace Carvajal.Turns.Utils.Interfaces
{
    public interface IMessageUtils
    {
        string GetTemplateMailRecoveryPasswor(string userName, string codeRecovery);
        string GetTemplateMailCreateMerchantInitial(string merchantName, string manufacturerName, string codeRecovery, string country);

        string GetTemplateRequestToManufacturer(string manufacturerContact, string name);
        string GetTemplatePointofSalesCreate(string manufacturerContact, string name);

        string GetTemplatePointofSalesVsUserCreate(string name);

        string GetTemplateRequestToMerchant(string manufacturer, string merchant);

    }
}
