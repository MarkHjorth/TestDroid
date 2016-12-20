using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Content;
using System.Collections.Generic;
using TestDroid.Logic.Controller;

namespace TestDroid
{
	[Activity(Label = "TestDroid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
        ListView logList;
        ArrayAdapter<string> arrayAdapter;
        List<string> logs;
        Logger logger;
		Button button_makeCall;

        Context context;

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
            logList = FindViewById<ListView>(Resource.Id.logList);
			button_makeCall = FindViewById<Button>(button_makeCall);

            logs = new List<string>();
            logs.Add("EVENTS:");
            arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, logs);
            logList.Adapter = arrayAdapter;
            logger = Logger.GetInstance(arrayAdapter);

            context = Application.Context;
        }

		private void AddHandlers()
		{
		}

		private void StartServer()
		{
			TCPserver tcpServer = new TCPserver(context);
		}
	}

}

