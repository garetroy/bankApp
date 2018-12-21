using System;
namespace bankLedger.Models
{
    public interface IHashService
    {
        Tuple<string, string> ComputeHash(string plainText);
        bool VerifyHash(string plainText, string hashValue, string salt);
    }
}
