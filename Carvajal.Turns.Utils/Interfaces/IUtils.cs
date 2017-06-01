using System;

namespace Carvajal.Turns.Utils.Interfaces
{
    public interface IUtils
    {
        string RandomString(int length);
        Guid GenereteUuid();
        object GetResponseMockup(string fileName);
        string GetRegexByCountry(string country);
        string GetResourceMessages(string Key);
    }
}
