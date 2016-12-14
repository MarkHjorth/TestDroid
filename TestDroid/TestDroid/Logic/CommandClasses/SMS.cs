using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;

namespace TestDroid
{
	public class SMS
	{
		private SmsManager smsManager;
        private Android.Net.Uri uri;
        private ICursor cursor;

        private static SMS instance;
        private static Context context;

        private SMS(Context cont)
		{
			smsManager = SmsManager.Default;
            context = cont;
		}

		//Calls and returns the only possible instance of SMS
		public static SMS GetInstance(Context c)
		{
			if (instance == null)
			{
				instance = new SMS(c);
			}
			return instance;
		}

		public bool SendSMS(string text, string phoneNumber)
		{
			bool succes = false;
            int countBefore = 0;
            int countAfter = 0;

            try
            {
                uri = Telephony.Sms.Inbox.ContentUri;
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                countBefore = cursor.Count;
            }
            catch (Exception e)
            {
                throw e;
            }

            try
			{
				
				smsManager.SendTextMessage(phoneNumber, null, text, null, null);
				succes = true;
			}
			catch
			{
				succes = false;
			}

            try
            {
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                countAfter = cursor.Count;
            }
            catch (Exception e)
            {
                throw e;
            }

            if (countBefore < countAfter)
            {
                succes = true;
            }
            else
            {
                succes = false;
            }
			return succes;
		}

	}
}
