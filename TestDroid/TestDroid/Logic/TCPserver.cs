﻿using System;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace TestDroid
{
	public class TCPserver
	{
		private Socket connection;
		private NetworkStream socketStream;
		private BinaryWriter writer;
		private BinaryReader reader;
		private TcpListener listener;
		private Controller controller;

		public TCPserver()
		{
			InitServer();
			StartServer();

		}

		private void InitServer()
		{
			try
			{
				//Step 1: create TcpListener (IPAddress skrives i long. Når den oversættes er det 127.0.0.1 (Localhost))
				IPAddress ip = new IPAddress(16777343);
				int port = 9001;
				listener = new TcpListener(ip, port);
				controller = new Controller();
			}

			catch(Exception error)
			{
				int i = 0;
			}
		}

		private void StartServer()
		{
			//Step 2: Tcp listener set to wait for connection request
			listener.Start();

			//Step 3: Establish connection on client request
			while (true)
			{
				connection = listener.AcceptSocket();

				socketStream = new NetworkStream(connection);

				writer = new BinaryWriter(socketStream);
				reader = new BinaryReader(socketStream);
				ServerClientInteraction();
			}
		}

		private void ServerClientInteraction()
		{

			string command = "";
			//Step 4: Read string data from client (command)
			do
			{

				try
				{
					command = reader.ReadString();

					switch (command)
					{
						case "sendSMS":
							controller.SendSMS();
							break;

						default:
							break;
					}
				}

				catch
				{

				}
			}
			while (command != "stop");
		}

	}
}