using System;
using Android.Telephony;

namespace TestDroid
{
	public class SMS
	{
		private SmsManager smsManager;
		private static SMS instance;
	
		private SMS()
		{
			smsManager = SmsManager.Default;

		}

		//Calls and returns the only possible instance of SMS
		public static SMS GetInstance()
		{
			if (instance == null)
			{
				instance = new SMS();
			}
			return instance;
		}

		public bool SendSMS(string text, string phoneNumber)
		{
			bool succes = false;

			try
			{
				
				smsManager.SendTextMessage(phoneNumber, null, text, null, null);
				succes = true;
			}
			catch
			{
				succes = false;
			}
			return succes;
		}

	}
}
