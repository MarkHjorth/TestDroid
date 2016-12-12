using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace TestDroid
{
	[Activity(Label = "TestDroid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			FindViews();
			AddHandlers();

			ThreadStart serverThreadStart = new ThreadStart(StartServer);
			Thread serverThread = new Thread(serverThreadStart);
			serverThread.Start();
 
		}
		private void FindViews()
		{ 
		}

		private void AddHandlers()
		{
		}

		private void StartServer()
		{
			TCPserver tcpServer = new TCPserver();
		}
	}

}

