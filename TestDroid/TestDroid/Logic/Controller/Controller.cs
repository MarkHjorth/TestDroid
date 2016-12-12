using System;
using Android.Telephony;
namespace TestDroid
{
	public class Controller
	{
		private SMS sms;

		public Controller()
		{
			
		}

		public bool SendSMS(string text = "Harambe did 9/11", string phoneNumber = "41618934")
		{
			sms = SMS.GetInstance();
			return sms.SendSMS(text, phoneNumber);
				
		}
	}
}
