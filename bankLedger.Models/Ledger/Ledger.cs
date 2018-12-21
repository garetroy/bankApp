using System;
using System.Linq;

namespace bankLedger.Models
{
    public class Ledger
    {
        public Ledger(TransactionType transactionType, decimal amount)
        {
            TransactionType = transactionType;
            Amount = amount;
        }

        public Ledger(int transactionType, decimal amount)
        {
            var enumValues = Enum.GetValues(typeof(TransactionType)).Cast<int>();
            var maxVal = enumValues.Max();
            var minVal = enumValues.Min();

            if(transactionType > maxVal || transactionType < minVal)
            {
                var errorString = "Given parameter transactionType "
                + $"= {transactionType} cannot be cast to Enum TransactionType";

                throw new ArgumentOutOfRangeException(errorString);
            }

            TransactionType = (TransactionType)transactionType;
            Amount = amount;
        }

        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
    }
}
