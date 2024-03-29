﻿using KursNBP.Lib;

using System;
using System.Linq;

namespace KursNBP.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CheckInput(args);
            }
            catch (Exception)
            {
                args = RepeatInput();
            }

            try
            {
                NBPExchangeRate exchange = new NBPExchangeRate(args[0].ToUpper(), DateTime.Parse(args[1]), DateTime.Parse(args[2]));
                System.Console.WriteLine(exchange);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("The system encountered an error.");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Please try again.");
            }
        }

        private static void CheckInput(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException();

            if (!AvailableCurrency.AvailabeCurrency.Any(x => x == args[0].ToUpper()))
                throw new ArgumentException("Not available or bad currency!");

            if (!DateTime.TryParse(args[1], out var startDate))
                throw new ArgumentException("Start date is in bad date format!");

            if (!DateTime.TryParse(args[2], out var endDate))
                throw new ArgumentException("End date is in bad date format!");

            if (startDate > endDate)
                throw new ArgumentException("End date cannot be later than start date!");

            if (startDate > DateTime.Now || endDate > DateTime.Now)
                throw new ArgumentOutOfRangeException("Unable to give a future date!");
        }

        private static string[] RepeatInput()
        {
            while(true)
            {
                System.Console.WriteLine("Bad input.");
                System.Console.WriteLine("Please pass correct input (Example: EUR 2018-09-01 2018-09-20)");
                try
                {
                    var input = System.Console.ReadLine();
                    var args = input.Replace(",", " ").Replace(".", " ").Split(" ");

                    CheckInput(args);

                    return args;
                }
                catch (Exception)
                {
                    System.Console.Clear();
                }
            }
            
        }
    }
}