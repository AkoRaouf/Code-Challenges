using System;
using System.Collections.Generic;
using System.Linq;

// https://www.sololearn.com/Discuss/729355/?ref=app
// https://www.freecodecamp.org/challenges/exact-change

namespace SoloLearn
{
    class Program
    {
        enum CoinCompartmentsType { Penny, Nickel, Dime, Quarter, One, Five, Ten, Twenty, Fifty, OneHundred }

        private class CoinCompartment
        {
            decimal HoldValue { get; set; }
            CoinCompartmentsType CoinCompartmentType { get; set; }
            public CoinCompartment(CoinCompartmentsType coinCompartmentType, decimal holdValue)
            {
                this.CoinCompartmentType = coinCompartmentType;
                this.HoldValue = holdValue;
            }
        }

        private class CashRegister
        {
            private readonly Dictionary<CoinCompartmentsType, decimal> CashDrawerDefination = new Dictionary<CoinCompartmentsType, decimal>
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

            private readonly Dictionary<CoinCompartmentsType, decimal> CashDrawerValues;

            public CashRegister(Dictionary<CoinCompartmentsType, decimal> cashDrawerValues)
            {
                CashDrawerValues = cashDrawerValues;
            }
        }

        static readonly Dictionary<CoinCompartmentsType, decimal> CashDrawer = new Dictionary<CoinCompartmentsType, decimal>
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

        static readonly Dictionary<CoinCompartmentsType, CoinCompartment> CashDrawer2 = new Dictionary<CoinCompartmentsType, CoinCompartment>
        {
            { CoinCompartmentsType.Penny,      new CoinCompartment( CoinCompartmentsType.Penny,    0.01m) },
            { CoinCompartmentsType.Nickel,     new CoinCompartment( CoinCompartmentsType.Nickel,   0.05m) },
            { CoinCompartmentsType.Dime,       new CoinCompartment( CoinCompartmentsType.Dime,     0.10m) },
            { CoinCompartmentsType.Quarter,    new CoinCompartment( CoinCompartmentsType.Quarter,  0.25m) },
            { CoinCompartmentsType.One,        new CoinCompartment( CoinCompartmentsType.One,         1m) },
            { CoinCompartmentsType.Five,       new CoinCompartment( CoinCompartmentsType.Five,        5m) },
            { CoinCompartmentsType.Ten,        new CoinCompartment( CoinCompartmentsType.Ten,        10m) },
            { CoinCompartmentsType.Twenty,     new CoinCompartment( CoinCompartmentsType.Twenty,     20m) },
            { CoinCompartmentsType.Fifty,      new CoinCompartment( CoinCompartmentsType.Fifty,      50m) },
            { CoinCompartmentsType.OneHundred, new CoinCompartment( CoinCompartmentsType.OneHundred,100m) }
        };

        static void Main(string[] args)
        {
            Console.Write("Case 1: ");
            Console.WriteLine(CheckCashRegister(3.26m, 100m, new Dictionary<CoinCompartmentsType, decimal>
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
            }));
            Console.Write("Case 2: ");
            Console.WriteLine(CheckCashRegister(19.50m, 20m, new Dictionary<CoinCompartmentsType, decimal>
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
            }));
            Console.Write("Case 3: ");
            Console.WriteLine(CheckCashRegister(19.50m, 70m, new Dictionary<CoinCompartmentsType, decimal>
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
            }));

            Console.ReadKey();
        }

        static string CheckCashRegister(decimal itemPrice, decimal cash, Dictionary<CoinCompartmentsType, decimal> cashInDrawer)
        {
            var changeAmount = cash - itemPrice;
            List<KeyValuePair<CoinCompartmentsType, decimal>> changeDue = GetChangeDue(cashInDrawer, ref changeAmount);

            return changeAmount != 0 ? // failed to fully pay the changeAmount?
                    "Insufficient Funds" :
                   cashInDrawer.Sum(c => c.Value) == 0 ? // any remaining cash in drawer?
                    "Closed" :
                    string.Join(", ", changeDue.Where(c => c.Value != 0));
        }

        private static List<KeyValuePair<CoinCompartmentsType, decimal>> GetChangeDue(Dictionary<CoinCompartmentsType, decimal> cashInDrawer, ref decimal changeAmount)
        {
            return new List<KeyValuePair<CoinCompartmentsType, decimal>>
            {
                PayoutChange(changeAmount, CoinCompartmentsType.OneHundred, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Fifty, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Twenty, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Ten, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Five, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.One, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Quarter, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Dime, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Nickel, cashInDrawer, out changeAmount),
                PayoutChange(changeAmount, CoinCompartmentsType.Penny, cashInDrawer, out changeAmount)
            };
        }

        static KeyValuePair<CoinCompartmentsType, decimal> PayoutChange(
            decimal changeAmount,
            CoinCompartmentsType compartmentType,
            Dictionary<CoinCompartmentsType, decimal> cashInDrawer,
            out decimal remainingChange)
        {
            remainingChange = changeAmount;

            bool IsDueByThisCoinCompartment = CashDrawer[compartmentType] > changeAmount;
            if (IsDueByThisCoinCompartment) return new KeyValuePair<CoinCompartmentsType, decimal>(compartmentType, 0);

            var quantity = Math.Floor(changeAmount / CashDrawer[compartmentType]);
            var cashValue = Math.Min(quantity * CashDrawer[compartmentType], cashInDrawer[compartmentType]);

            cashInDrawer[compartmentType] -= cashValue;
            remainingChange = changeAmount - cashValue;

            return new KeyValuePair<CoinCompartmentsType, decimal>(compartmentType, cashValue);
        }
    }
}