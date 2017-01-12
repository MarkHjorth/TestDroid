using System;

namespace TestDroidClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Controller ctrl = new Controller(args);
            string input = "";
            string[] inputArgs;

            if (!(args.Length >= 1))
            {
                while(input != "exit")
                {
                    input = Console.ReadLine();
                    inputArgs = input.Split(' ');
                    ctrl.ParseCommand(inputArgs);
                } 
            }
            return;
        }
	}
}
