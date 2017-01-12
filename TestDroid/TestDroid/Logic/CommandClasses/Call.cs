using System;
using Android.Content;
using TestDroid.Logic.Controller;
using Android.Net;
using Android.Telephony;
using Android.Widget;
using Android.Provider;

namespace TestDroid
{
	public class Call
	{
		string phonenumber;
		Context context;
		Logger logger;
//		CellSignalStrength signal;

		public Call(Context context)
		{
			this.logger = Logger.GetInstance();
			this.context = context;
		}

		public bool MakeCall(string[] args)
		{
			bool didSucceed = false;
            try
            {
                phonenumber = args[3];
            }
            catch
            {
                logger.LogEvent("No phone number found. Using default", 2);
                phonenumber = "71840913";
            }    
			string url = "tel:" + phonenumber;
			try
			{
				if (IsFlightmodeOn())
				{
					didSucceed = false;
					logger.LogEvent("Call did not succeed, flightmode on", 2);
					return didSucceed;
				}

                logger.LogEvent("Calling: " + phonenumber, 0);
                //Intent is android specific. Gives ActionCall as option and takes phonenumber through 2. arg
                Intent intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(url));

				context.StartActivity(intent);
				didSucceed = true;
                logger.LogEvent("Call succeeded");
            }
			catch (Exception ex)
			{
				//level of log: 0 info, 1 debug, 2 warning, 3 error, 4 fatal
				logger.LogEvent(ex.Message, 3);
				didSucceed = false;
			}
			return didSucceed;
		}


		public bool IsFlightmodeOn()
		{
			bool boolFlightmode = Settings.System.GetInt(context.ContentResolver, Settings.Global.AirplaneModeOn, 0) != 0;
			return boolFlightmode;

		}
	}
}
