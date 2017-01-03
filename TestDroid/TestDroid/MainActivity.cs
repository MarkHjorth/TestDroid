using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Content;
using System.Collections.Generic;
using TestDroid.Logic.Controller;
using Android.Content.PM;

namespace TestDroid
{
	[Activity(Label = "TestDroid", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
        ListView logList;
        ArrayAdapter<string> arrayAdapter;
        List<string> logs;
        Logger logger;
		Context context;
		Button button_makeCall;
		ButtonHandlers buttonHandler;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			context = this;
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
			logs = new List<string>();
			logs.Add("EVENTS:");
			arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, logs);
			logList.Adapter = arrayAdapter;
			logger = Logger.GetInstance(arrayAdapter);
			logger.LogEvent("It works here", 4);

			buttonHandler = new ButtonHandlers(context);

           
			button_makeCall = FindViewById<Button>(Resource.Id.button_makeCall); 
        }


		private void AddHandlers()
		{
			button_makeCall.Click += buttonHandler.ButtonMakeCallHandler;
		}

		private void StartServer()
		{
			TCPserver tcpServer = new TCPserver(context);
		}
	}

}

