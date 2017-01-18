using System;
using System.Collections.Generic;
using TestDroidClient.Controllers;
using TestDroidClient.Models;

namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;
		private Flightmode flightmode;
		private Call call;
        private Sms sms;
        private ApkInstaller apk;
        private bool stopConnection = false;
        
        public Controller(string[] args)
		{
            try
            {
				flightmode = new Flightmode();
				call = new Call();
                sms = new Sms();
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

        /// <summary>
        /// Parses the command on to the correct subcontroller
        /// </summary>
        /// <param name="args">Command as string array</param>
		public void ParseCommand(string[] args)
		{
            string command = args[0];
            int id = (int) GenerateId();
            string fullCommand = (id + " " + string.Join(" ", args));

            switch (command)
			{
				case "sms"://Other cases here
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.Stop = stopConnection;
                    sms.HandleSms(id, args);
                    break;
				case "flightmode":
					flightmode.HandleFlightmode(args);
                    break;
				case "call":
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.Stop = stopConnection;
                    call.HandleCall(args, id);
					break;
                case "installapk":
                    apk.InstallApk();
				    break;
				default:
					Console.WriteLine("Command not found!");
					break;
			}
		}

        /// <summary>
        /// Generates a unique ID for the command
        /// </summary>
        /// <returns>long ID</returns>
        private long GenerateId()
        {
            long id = DateTime.Now.Ticks;
            return id;
        }
	}
}
