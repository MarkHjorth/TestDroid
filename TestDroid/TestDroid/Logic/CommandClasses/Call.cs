﻿using System;
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
				//Intent is android specific. Gives ActionCall as option and takes phonenumber through 2. arg
				Intent intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(url));

				context.StartActivity(intent);
				didSucceed = true;
				logger.LogEvent("calling tel: " + phonenumber, 0); 

			}
			catch (Exception ex)
			{
				//level of log: 0 info, 1 debug, 2 warning, 3 error, 4 fatal
				logger.LogEvent(ex.Message, 3);
				didSucceed = false;
			}

			return didSucceed;
		}

	}
}