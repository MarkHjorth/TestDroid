using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;

		public Controller(string[] args)
		{
			string command = string.Join(" ", args);
            try
            {
                tcpConnection = new TCPConnection();
				ParseCommand(command);
            }
            catch (Exception e)
            {
				Console.WriteLine(e.StackTrace);
            }
		}

		public void ParseCommand(string fullCommand)
		{
			string[] args = fullCommand.Split(' ');
			string command = args[0];
			try
			{
				if (command == "sendSMS")
				{
					bool worked = tcpConnection.SendCommand(fullCommand);

					if (!worked)
					{
						Console.WriteLine("Command not send!");
					}
				}
				else
				{
					Console.WriteLine("Command not found!");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
