using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;

		public Controller()
		{
			tcpConnection = new TCPConnection();
		}

		public void ParseCommand(string fullCommand)
		{
			string[] args = fullCommand.Split(' ');
			string command = args[0];
			if (command == "sendSMS")
			{
				if (tcpConnection.SendCommand(fullCommand))
				{
					Console.WriteLine("Prolihøhar");
				}
			}
			else
			{
				Console.WriteLine("Command not found!");
			}
		}
	}
}
