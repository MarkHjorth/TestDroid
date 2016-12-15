using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;
using TestDroid.Logic.Controller;
using Android.App;
using System.Threading;

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
                uri = Telephony.Sms.ContentUri;
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                countBefore = cursor.Count;
                logger.LogEvent(countBefore.ToString());
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
			catch(Exception e)
			{
                logger.LogEvent(e.StackTrace, 3);
				succes = false;
			}

            try
            {
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                countAfter = cursor.Count;
                logger.LogEvent(countAfter.ToString());
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
            Thread t = new Thread(new ThreadStart(CountSms));
            t.Start();
			return succes;
		}

        private void CountSms()
        {
            int before = 0;
            int after = 0;
            uri = Telephony.Sms.ContentUri;
            cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
            before = cursor.Count;
            logger.LogEvent(before.ToString());

            int i = 0;
            while (i < 1000)
            {
                uri = Telephony.Sms.ContentUri;
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                after = cursor.Count;
                if (before != after)
                {
                    i = 9001;
                    logger.LogEvent("Text sent!");
                    return;
                }
                i++;
            }
            logger.LogEvent("Text not sent");
        }
    }
}
