using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace TestDroidClient
{
	public class TCPConnection
	{
		private NetworkStream output;
		private BinaryWriter writer;
		private BinaryReader reader;
		private TcpClient client;


		public TCPConnection()
		{
			int remotePort = 9001;
			int localPort = 9001;

            bool forwarded = PortForward(remotePort, localPort);
            if (!forwarded)
            {
                Console.WriteLine("Not forw");
                throw new Exception();
            }
            Console.WriteLine("forw");
            InitClient(remotePort);

		}

		private bool PortForward(int remotePort, int localPort)
		{
            try
            {
                string path = "/Users/dina/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb";
                string parameters = string.Format("-d forward tcp:{0} tcp:{1}", remotePort, localPort);
                Process.Start(path, parameters);
            }
			catch(Exception e)
            {
				throw e;
            }
            try
			{
				string path = "C:/Program Files (x86)/Android/android-sdk/platform-tools/adb.exe";
				string parameters = string.Format("-d Forward tcp:{0} tcp:{1}", remotePort, localPort);
				Process.Start(path, parameters);
				return true;
			}
			catch(Exception e)
			{
				throw e;
			}
		
		}

		private void InitClient(int remotePort)
		{
			Thread readThread = new Thread(() => RunClient(remotePort));
			readThread.Start();

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

			}
			catch(Exception e)
			{
				throw e;
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
					throw e;
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
			try
			{
				writer.Write(command);
				return true;
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}
}
