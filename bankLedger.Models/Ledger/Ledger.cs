using System;
using System.Collections.Generic;
using System.Linq;

namespace bankLedger.Models
{
    public class Ledger
    {
        public Ledger(ulong ledgerId, TransactionType transactionType, decimal amount)
        {
            LedgerId = ledgerId;
            TransactionType = transactionType;
            Amount = amount;
        }

        public Ledger(ulong ledgerId, int transactionType, decimal amount)
        {
            IEnumerable<int> enumValues = Enum.GetValues(typeof(TransactionType)).Cast<int>();
            int maxVal = enumValues.Max();
            int minVal = enumValues.Min();

            if (transactionType > maxVal || transactionType < minVal)
            {
                var errorString = "Given parameter transactionType "
                + $"= {transactionType} cannot be cast to Enum TransactionType";

                throw new ArgumentOutOfRangeException(errorString);
            }

            LedgerId = ledgerId;
            TransactionType = (TransactionType)transactionType;
            Amount = amount;
        }

        public ulong LedgerId { get; }
        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
    }
}