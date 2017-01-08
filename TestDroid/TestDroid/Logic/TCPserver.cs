using System;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Android.Content;
using TestDroid.Logic.Controller;
using System.Threading.Tasks;

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
        private Logger logger;

		public TCPserver(Context context)
		{
			InitServer(context);
			StartServer();
		}

		private void InitServer(Context context)
		{
			try
			{
				//Step 1: create TcpListener (IPAddress skrives i long. Når den oversættes er det 127.0.0.1 (Localhost))
				IPAddress ip = new IPAddress(16777343);
				int port = 9001;
				listener = new TcpListener(ip, port);
				controller = new Controller(context);
                logger = Logger.GetInstance();
			}

			catch(Exception e)
			{
                logger.LogEvent(e.StackTrace, 3);
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
            logger.LogEvent("Client connected!");

            string fullCommand = "";
            string command = "";
            
            //Step 4: Read string data from client (command)
            do
			{
				try
				{
					fullCommand = reader.ReadString();
                    command = fullCommand.Split(' ')[1];
                    Task commandTask = RunCommand(fullCommand);
                    //commandTask.Start();
                }
				catch (EndOfStreamException)
				{
                    logger.LogEvent("Connection lost");
                    return;
				}
                catch(Exception e)
                {
                    logger.LogEvent(e.StackTrace);
                    return;
                }
			}
			while (command != "stop");
		}

        private async Task RunCommand(string fullCommand)
        {
            string id;
            string[] args;
            string command;
            bool success;
            try
            {
                args = fullCommand.Split(' ');
                id = args[0];
                command = args[1];
                success = false;

                switch (command)
                {
                    case "sendSMS":
                        success = controller.SendSMS(args);
                        break;
                    case "call":
                        //success = controller.MakeCall(args);
                        break;
                    case "stop":
                        writer.Write("stop");
                        break;
                    default:
                        break;
                }
                Respond(id, success);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Respond(string id, bool success)
        {
            string response = string.Format("{0} {1}", id, success);

            writer.Write(response);
        }
	}
}
