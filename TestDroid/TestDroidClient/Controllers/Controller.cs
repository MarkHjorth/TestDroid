using System;
using System.Collections.Generic;
using TestDroidClient.Models;

namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;
		private Flightmode flightmode;
        private ApkInstaller apk;
        private bool stopConnection = false;

        

        public Controller(string[] args)
		{
            try
            {
				flightmode = new Flightmode();
                apk = new ApkInstaller();

                if (args.Length >= 1)
                {
                    stopConnection = true;
                    ParseCommand(args);
                }
            }
            catch (Exception e)
            {
				Console.WriteLine(e.StackTrace);
            }
		}

		public void ParseCommand(string[] args)
		{
            string command = args[0];
            long id = GenerateId();
            string fullCommand = (id + " " + string.Join(" ", args));

			switch (command)
			{
				case "sendSMS"://Other cases here
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.Stop = stopConnection;
                    tcpConnection.SendCommand(id, fullCommand);
					break;
				case "flightmode":
					flightmode.HandleFlightmode(args);
                    break;
                case "installApk":
                    apk.InstallApk();
				    break;
				default:
					Console.WriteLine("Command not found!");
					break;
			}
		}

        private long GenerateId()
        {
            long id = DateTime.Now.Ticks;
            return id;
        }
	}
}