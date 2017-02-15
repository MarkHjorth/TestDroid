using System;

namespace TestDroidClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Controller ctrl = new Controller(args);
            string input = "";
            string lowerInput = "";
            string[] inputArgs;

            if (!(args.Length >= 1))
            {
				do
				{
					input = Console.ReadLine();
					lowerInput = input.ToLower();
					inputArgs = lowerInput.Split(' ');
					ctrl.ParseCommand(inputArgs);
				} while (lowerInput != "exit");
            }
            return;
        }
	}
}
