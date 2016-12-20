    using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

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

        public bool GotIO { get; set; }

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

        public static TCPConnection GetInstance()
        {
            if(instance == null)
            {
                instance = new TCPConnection();
            }
            return instance;
        }

		private void PortForward(int remotePort, int localPort)
		{
            try
            {
                string parameters = string.Format("-d forward tcp:{0} tcp:{1}", remotePort, localPort);
                adb.startProcess(parameters);
            }
			catch(Exception e)
            {
				throw e;
            }
            Console.WriteLine("Port forwarded");
		}

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
					Console.WriteLine(message);
				}
				catch(Exception e)
				{
                    Console.WriteLine(e.StackTrace);
                    break;
				}

			}
			while (message != "stop");
			try
			{
				writer.Close();
				reader.Close();
				output.Close();
				client.Close();
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		public bool SendCommand(string command)
		{
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

			try
			{
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
