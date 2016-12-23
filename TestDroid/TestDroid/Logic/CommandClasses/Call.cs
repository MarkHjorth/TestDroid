using System;
using Android.Content;
using TestDroid.Logic.Controller;

namespace TestDroid
{
	public class Call
	{
		
		string phonenumber = "41618934";
		Context context;
		Logger logger;

		public Call(Context context)
		{
			this.logger = Logger.GetInstance();
			this.context = context;
		}

		public bool MakeCall()
		{
			bool didSucceed = false;
			string url = "tel:" + phonenumber;
			try
			{
				Intent intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(url));

				context.StartActivity(intent);
				didSucceed = true;

			}
			catch (Exception ex)
			{
				logger.LogEvent(ex.Message, 0);
				didSucceed = false;
			}

			return didSucceed;
		}

	}
}
