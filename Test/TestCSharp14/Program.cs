using System;

namespace TestCSharp14
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //---------------------
            // field關鍵字
            Console.WriteLine($"1a. field關鍵字: {Settings??"null"}");
            Settings = "AI行動股神";
            Console.WriteLine($"1b. field關鍵字: {Settings ?? "null"}");
            //---------------------
            // 空條件賦值
            Logger Logger = null;

            Console.WriteLine();
            Logger?.LastMessage = "Hello World";
            Console.WriteLine($"2a. 空條件賦值: {Logger?.LastMessage ?? "null"}");

            Logger = new Logger();
            Logger?.LastMessage = "AI行動股神 haha";
            Console.WriteLine($"2b. 空條件賦值: {Logger?.LastMessage ?? "null"}");
            //---------------------
            Console.WriteLine();
            Console.WriteLine("3. Lambda 表達式");

            var printValue = (in int x) => x+1;
            int num = 42;
            printValue(num); // Output: 42
            Console.WriteLine($"3a. in: {num}"); // Output: 42

            var doubleValue = (ref int x) => x *= 2;
            int val = 5;
            doubleValue(ref val);
            Console.WriteLine($"3b. ref: {val}"); // Output: 10

            var tryParse = (string s, out int result) => int.TryParse(s, out result);
            tryParse("123", out int value);
            Console.WriteLine($"3c. out: {value}"); // Output: 123
            //-----------
            var trimValidate = (string input, out string result) =>
            {
                result = input?.Trim() ?? "";
                return !string.IsNullOrEmpty(result) && result.Length <= 20;
            };

            // Mask sensitive data
            var maskSensitive = (ref string data) =>
            {
                if (data.Length > 4)
                    data = new string('*', data.Length - 4) + data[^4..];
            };

            string[] inputs = { " 1234567890 ", "abcd", "   ", "98765" };

            foreach (var input in inputs)
            {
                if (trimValidate(input, out string cleaned))
                {
                    maskSensitive(ref cleaned);
                    Console.WriteLine($"3d. Processed input({input}): {cleaned}");
                }
                else
                {
                    Console.WriteLine($"3d. Invalid input({input})");
                }
            }
            //-----------
            //---------------------

            //Console.ReadKey();
        }        
        //-----------------------------------------------------
        // field關鍵字
        static string Settings
        {
            get => field ??= LoadSettings();
            set => field = value;
        }

        static string LoadSettings()
        {
            return "Config Loaded";
        }
        //-----------------------------------------------------
        // 空條件賦值
        class Logger
        {
            public string LastMessage;
        }
        //-----------------------------------------------------        
    }
}
