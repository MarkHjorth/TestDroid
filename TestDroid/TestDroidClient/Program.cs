using System;

namespace TestDroidClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Controller ctrl = new Controller(args);
            do
            {
                ctrl.ParseCommand(Console.ReadLine());
            } while (true);
        }
	}
}
