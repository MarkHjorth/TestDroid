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
		private Power power;
        private Sms sms;
        private ApkInstaller apk;
        private bool stopConnection = false;
        
        public Controller(string[] args)
		{
            try
            {
				flightmode = new Flightmode();
				call = new Call();
				power = new Power();
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
		public bool ParseCommand(string[] args)
		{
			bool didSucceed = false;
			string command = args[0];
            int id = (int) GenerateId();
            string fullCommand = (id + " " + string.Join(" ", args));

            switch (command)
			{
				case "sms"://Other cases here
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.Stop = stopConnection;
					didSucceed = sms.HandleSms(id, args).Wait(3000);
                    break;
				case "flightmode":
					didSucceed = flightmode.HandleFlightmode(args);
                    break;
				case "call":
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.Stop = stopConnection;
                    didSucceed = call.HandleCall(args, id).Wait(3000);
					break;
				case "power":
					tcpConnection = TCPConnection.GetInstance();
					tcpConnection.Stop = stopConnection;
					didSucceed = power.HandlePower(args, id).Wait(3000);
					break;
                case "installapk":
                    didSucceed = apk.InstallApk();
				    break;
				default:
					Console.WriteLine("Command not found!");
					break;
			}

			return didSucceed;
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
