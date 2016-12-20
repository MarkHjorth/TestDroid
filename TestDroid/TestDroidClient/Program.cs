using System;

namespace TestDroidClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Controller ctrl = new Controller(args);

            if (!(args.Length >= 1))
            {
                do
                {
                    string[] inputArgs = Console.ReadLine().Split(' ');
                    ctrl.ParseCommand(inputArgs);
                } while (true); 
            }
        }
	}
}
