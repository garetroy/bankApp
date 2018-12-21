using System;
using bankLedger.Data.DbObject;
using bankLedger.Models;

namespace bankLedger.Data.Mappers
{
    internal sealed class AccountMapper
    {
        public Account Map(DbAccount account)
        {
            return new Account(
                account.User_Name,
                account.Encrypted_Password,
                account.Salt,
                account.Last_Login);
        }
    }
}
