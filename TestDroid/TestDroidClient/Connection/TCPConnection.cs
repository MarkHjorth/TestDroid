using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using TestDroidClient.Models;

namespace TestDroidClient
{
	public class TCPConnection
	{
        private static TCPConnection instance;

		private NetworkStream output;
		private BinaryWriter writer;
		private BinaryReader reader;
		private TcpClient client;
        private ADBhandler adb;
        Dictionary<int, CommandClass> commandArray = new Dictionary<int, CommandClass>();

        public bool GotIO { get; set; }
        public bool Stop { get; set; }

        /// <summary>
        /// The TCP constructor. Calls the methods to start the TCP connection
        /// </summary>
        private TCPConnection()
		{
            adb = new ADBhandler();
            GotIO = false;
			int remotePort = 9001;
			int localPort = 9001;

            try
            {
                PortForward(remotePort, localPort);
                InitClient(remotePort);
            }
            catch (Exception e)
            {
                throw e;
            }
		}

        /// <summary>
        /// Singleton GetInstance() method
        /// </summary>
        /// <returns>returns singleton instance of TCPConnection</returns>
        public static TCPConnection GetInstance()
        {
            if(instance == null)
            {
                instance = new TCPConnection();
            }
            return instance;
        }

        /// <summary>
        /// Forwarding the ports needed for TCP connection
        /// </summary>
        /// <param name="remotePort">Port to talk to on server</param>
        /// <param name="localPort">Port to use internally</param>
		private void PortForward(int remotePort, int localPort)
		{
            try
            {
                string parameters = string.Format("-d forward tcp:{0} tcp:{1}", remotePort, localPort);
                adb.StartProcess(parameters);
            }
			catch(Exception e)
            {
				throw e;
            }
            Console.WriteLine("Port forwarded");
		}

        /// <summary>
        /// Initialize the client connection to the server
        /// </summary>
        /// <param name="remotePort">The port on the server to atempt to connect to</param>
		private void InitClient(int remotePort)
		{
            try
            {
                Thread readThread = new Thread(() => RunClient(remotePort));
                readThread.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
		}

        /// <summary>
        /// Creates the actual TCP connectionand connects it to the server, on the specified port
        /// Reads messages from the server
        /// </summary>
        /// <param name="port">Server port to connect to</param>
		private void RunClient(int port)
		{
			string message = "";

			try
			{
				client = new TcpClient();
				client.Connect("localhost", port);
				output = client.GetStream();
				writer = new BinaryWriter(output);
				reader = new BinaryReader(output);
				Console.WriteLine("Got IO Streams");
                GotIO = true;
			}
			catch(Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}

			do
			{
				try
				{
					message = reader.ReadString();
                    string[] messageArgs = message.Split(' ');
                    try
                    {
                        int id = int.Parse(messageArgs[0]);

                        CommandClass cmd = commandArray[id];

                        string command = cmd.Command.Split(' ')[1];
                        string worked = messageArgs[1].ToLower();
                        worked = (worked == "true") ? "Success" : "Failed";
                        string result = string.Format("Command: {0} {1}: {2}", id, command, worked);

                        Console.WriteLine(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                    if (Stop && message != null && message != "Connection established!")
                    {
                        message = "stop";
                    }
                }
				catch(Exception e)
				{
                    Console.WriteLine(e.StackTrace);
                    break;
				}
			}
			while (message.ToLower() != "stop");

			try
			{
				writer.Close();
				reader.Close();
				output.Close();
				client.Close();
                Environment.Exit(0);
			}
			catch(Exception e)
			{
				throw e;
			}
		}

        /// <summary>
        /// Attempts to send the command to the server
        /// </summary>
        /// <param name="command">The command to send to the server</param>
        /// <returns>True if command sent; False if command NOT sent</returns>
		public bool SendCommand(int id, string command)
		{
            // Waits for the esrver to be ready to recieve command
            for (int i = 0; i < 5; i++)
            {
                if (writer == null && i != 4)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Waiting for connection to server");
                }
                else
                {
                    i = 5;
                }
                if(i == 4)
                {
                    Console.WriteLine("No connection could be established! Check the server!");
                    return false;
                }
            }

            // Sending the command to the server
			try
			{
                DateTime time = DateTime.Now;
                CommandClass cmd = new CommandClass(id, command, time);
                commandArray.Add(id, cmd);
                writer.Write(command);
                Console.WriteLine("Command: '" + command + "' sent to server");
				return true;
			}
			catch(Exception e)
			{
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("No connection");
                return false;
			}
		}
	}
}
