using System;
using Android.Telephony;
using Android.Content;

namespace TestDroid
{
	public class Controller
	{
		private SMS sms;
        Context context;

		public Controller(Context cont)
		{
            context = cont;
		}

		public bool SendSMS(string text = "Harambe did 9/11", string phoneNumber = "41618934")
		{
			sms = SMS.GetInstance(context);
			return sms.SendSMS(text, phoneNumber);
				
		}
	}
}
