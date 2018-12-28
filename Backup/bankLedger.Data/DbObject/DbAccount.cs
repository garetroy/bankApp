using System;
namespace bankLedger.Data.DbObject
{
    internal sealed class DbAccount
    {
        public string User_Name { get; set; }
        public string Encrypted_Password { get; set; }
        public string Salt { get; set; }
        public DateTime? Last_Login { get; set; }
    }
}
