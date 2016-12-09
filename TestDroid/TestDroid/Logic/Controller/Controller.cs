using System;
using Android.Telephony;
namespace TestDroid
{
	public class Controller
	{
		private SmsManager smsManager;

		public Controller()
		{
			
		}

		public bool SendSMS(string text = "Bush Did 9/11")
		{
			bool succes = false;
			string phoneNumber = "41618934";


			try
			{
				smsManager = SmsManager.Default;
				smsManager.SendTextMessage(phoneNumber, null, text, null, null);
			}
			catch
			{ 
			}

		}
	}
}
