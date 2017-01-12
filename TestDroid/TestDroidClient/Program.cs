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
                    string input = Console.ReadLine();
                    string lowerInput = input.ToLower();
                    string[] inputArgs = lowerInput.Split(' ');
                    ctrl.ParseCommand(inputArgs);
                } 
            }
            return;
        }
	}
}
