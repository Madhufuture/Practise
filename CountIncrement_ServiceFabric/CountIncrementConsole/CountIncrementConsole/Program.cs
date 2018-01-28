using System;

namespace CountIncrementConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int i;

            for (i = 1; i <= 5; i++)
            {
                Console.WriteLine(i);
                if (i % 5 != 0) continue;
                Console.WriteLine("Do you want to continue (y/n)");
                var response = Console.ReadLine();
                if (Convert.ToChar(response) == 'y')
                {
                    i = 0;
                    continue;
                }
                break;
            }
        }
    }
}
