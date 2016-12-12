using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;

		public Controller()
		{
            try
            {
                tcpConnection = new TCPConnection();
            }
            catch (Exception)
            {
                Console.WriteLine("ADB not found!");
            }
		}

		public void ParseCommand(string fullCommand)
		{
			string[] args = fullCommand.Split(' ');
			string command = args[0];
			if (command == "sendSMS")
			{
                bool worked = tcpConnection.SendCommand(fullCommand);

                if(!worked)
				{
					Console.WriteLine("Command not send!");
				}
			}
			else
			{
				Console.WriteLine("Command not found!");
			}
		}
	}
}
