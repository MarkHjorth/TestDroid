using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;

		public Controller(string[] args)
		{
            try
            {
                tcpConnection = new TCPConnection();

                while(!tcpConnection.GotIO)
                { }

                if (args.Length >= 1)
                {
                    string command = string.Join(" ", args);
                    ParseCommand(command);
                }
            }
            catch (Exception e)
            {
				Console.WriteLine(e.StackTrace);
            }
		}

		public void ParseCommand(string fullCommand)
		{
			string[] args = fullCommand.Split(' ');
			string command = args[0];
			try
			{
                switch (command)
                {
                    case "sendSMS":
                        bool worked = tcpConnection.SendCommand(fullCommand);

                        if (!worked)
                        {
                            Console.WriteLine("Command not send!");
                        }
                        break;
                    case "installAPK":
                        InstallApk();
                        break;
                    default:
                        break;
                }
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

        private bool InstallApk()
        {
            string adbPath = "C:/Program Files (x86)/Android/android-sdk/platform-tools/adb.exe";
            string apkName = "com.rohde_schwarz.testdroid.apk";

            ApkInstaller installer = new ApkInstaller();
            return installer.InstallApk(adbPath, apkName);
        }
	}
}
