using System;
using System.Collections.Generic;
using System.Linq;

// https://www.sololearn.com/Discuss/729355/?ref=app
// https://www.freecodecamp.org/challenges/exact-change

namespace SoloLearn2
{
    class Program2
    {
        enum CoinCompartmentsType { Penny, Nickel, Dime, Quarter, One, Five, Ten, Twenty, Fifty, OneHundred }
      
        private static class CashDrawerDefination
        {
            private static readonly Dictionary<CoinCompartmentsType, decimal> Defination = new Dictionary<CoinCompartmentsType, decimal>
            {
                { CoinCompartmentsType.Penny,     0.01m },
                { CoinCompartmentsType.Nickel,    0.05m },
                { CoinCompartmentsType.Dime,      0.10m },
                { CoinCompartmentsType.Quarter,   0.25m },
                { CoinCompartmentsType.One,          1m },
                { CoinCompartmentsType.Five,         5m },
                { CoinCompartmentsType.Ten,         10m },
                { CoinCompartmentsType.Twenty,      20m },
                { CoinCompartmentsType.Fifty,       50m },
                { CoinCompartmentsType.OneHundred, 100m }
            };

            public static decimal GetValue(CoinCompartmentsType coinCompartmentsType)
            {
                return Defination[coinCompartmentsType];
            }
        }

        private class CashRegister
        {
            public readonly Dictionary<CoinCompartmentsType, decimal> CashDrawerValues;

            public CashRegister(Dictionary<CoinCompartmentsType, decimal> cashDrawerValues)
            {
                CashDrawerValues = cashDrawerValues;
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Case 1: ");
            var cashRegiter1 = new CashRegister(new Dictionary<CoinCompartmentsType, decimal>
            {
                { CoinCompartmentsType.Penny,     1.01m },
                { CoinCompartmentsType.Nickel,    2.05m },
                { CoinCompartmentsType.Dime,      3.10m },
                { CoinCompartmentsType.Quarter,   4.25m },
                { CoinCompartmentsType.One,         90m },
                { CoinCompartmentsType.Five,        55m },
                { CoinCompartmentsType.Ten,         20m },
                { CoinCompartmentsType.Twenty,      60m },
                { CoinCompartmentsType.Fifty,       50m },
                { CoinCompartmentsType.OneHundred, 100m }
            });

            Console.WriteLine(CheckCashRegister(3.26m, 100m, cashRegiter1));
            Console.Write("Case 2: ");

            var cashRegister2 = new CashRegister(new Dictionary<CoinCompartmentsType, decimal>
            {
                { CoinCompartmentsType.Penny,  0.01m },
                { CoinCompartmentsType.Nickel,     0 },
                { CoinCompartmentsType.Dime,       0 },
                { CoinCompartmentsType.Quarter,    0 },
                { CoinCompartmentsType.One,       1m },
                { CoinCompartmentsType.Five,       0 },
                { CoinCompartmentsType.Ten,        0 },
                { CoinCompartmentsType.Twenty,     0 },
                { CoinCompartmentsType.Fifty,    50m },
                { CoinCompartmentsType.OneHundred, 0 }
            });

            Console.WriteLine(CheckCashRegister(19.50m, 20m, cashRegister2));
            Console.Write("Case 3: ");

            var cashRegister3 = new CashRegister(new Dictionary<CoinCompartmentsType, decimal>
            {
                { CoinCompartmentsType.Penny,  0.50m },
                { CoinCompartmentsType.Nickel,     0 },
                { CoinCompartmentsType.Dime,       0 },
                { CoinCompartmentsType.Quarter,    0 },
                { CoinCompartmentsType.One,        0 },
                { CoinCompartmentsType.Five,       0 },
                { CoinCompartmentsType.Ten,      30m },
                { CoinCompartmentsType.Twenty,   20m },
                { CoinCompartmentsType.Fifty,      0 },
                { CoinCompartmentsType.OneHundred, 0 }
            });

            Console.WriteLine(CheckCashRegister(19.50m, 70m, cashRegister3));

            Console.ReadKey();
        }

        static string CheckCashRegister(decimal itemPrice, decimal cash, CashRegister cashRegister)
        {
            var changeAmount = cash - itemPrice;
            List<KeyValuePair<CoinCompartmentsType, decimal>> changeDue = GetChangeDue(cashRegister, ref changeAmount);

            return changeAmount != 0 ? // failed to fully pay the changeAmount?
                    "Insufficient Funds" :
                   cashRegister.CashDrawerValues.Sum(c => c.Value) == 0 ? // any remaining cash in drawer?
                    "Closed" :
                    string.Join(", ", changeDue.Where(c => c.Value != 0));
        }

        private static List<KeyValuePair<CoinCompartmentsType, decimal>> GetChangeDue(CashRegister cashRegister, ref decimal changeAmount)
        {
            return new List<KeyValuePair<CoinCompartmentsType, decimal>>
            {
                Payout(changeAmount, CoinCompartmentsType.OneHundred, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Fifty, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Twenty, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Ten, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Five, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.One, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Quarter, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Dime, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Nickel, cashRegister, out changeAmount),
                Payout(changeAmount, CoinCompartmentsType.Penny, cashRegister, out changeAmount)
            };
        }

        static KeyValuePair<CoinCompartmentsType, decimal> Payout(
            decimal changeAmount,
            CoinCompartmentsType compartmentType,
            CashRegister cashRegister,
            out decimal remainingChange)
        {
            remainingChange = changeAmount;

            bool IsDueByThisCoinCompartment = CashDrawerDefination.GetValue(compartmentType) > changeAmount;
            if (IsDueByThisCoinCompartment) return new KeyValuePair<CoinCompartmentsType, decimal>(compartmentType, 0);

            var quantity = Math.Floor(changeAmount / CashDrawerDefination.GetValue(compartmentType));
            var cashValue = Math.Min(quantity * CashDrawerDefination.GetValue(compartmentType), cashRegister.CashDrawerValues[compartmentType]);

            cashRegister.CashDrawerValues[compartmentType] -= cashValue;
            remainingChange = changeAmount - cashValue;

            return new KeyValuePair<CoinCompartmentsType, decimal>(compartmentType, cashValue);
        }
    }
}