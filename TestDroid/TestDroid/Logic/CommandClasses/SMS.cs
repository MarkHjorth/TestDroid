using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;
using TestDroid.Logic.Controller;

namespace TestDroid
{
	public class SMS
	{
		private SmsManager smsManager;
        private Android.Net.Uri uri;
        private ICursor cursor;

        private static SMS instance;
        private static Context context;
        
        Logger logger;

        private SMS(Context cont)
		{
			smsManager = SmsManager.Default;
            context = cont;
            logger = Logger.GetInstance();
		}

        /// <summary>
        /// Calls and returns the only possible instance of SMS
        /// </summary>
        /// <param name="c">The Context</param>
        /// <returns></returns>
        public static SMS GetInstance(Context c)
		{
			if (instance == null)
			{
				instance = new SMS(c);
			}
			return instance;
		}

        /// <summary>
        /// Sends an SMS
        /// </summary>
        /// <param name="text">The text (Body) of the SMS message</param>
        /// <param name="phoneNumber">The phone number to send the SMS to</param>
        /// <returns>True if the SMS is send</returns>
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
                logger.LogEvent(e.StackTrace, 3);
            }

            try
			{
                logger.LogEvent("Sending SMS to: " + phoneNumber);
				smsManager.SendTextMessage(phoneNumber, null, text, null, null);
				succes = true;
			}
			catch
			{
                logger.LogEvent("SMS not sent", 2);
				succes = false;
			}

            try
            {
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                countAfter = cursor.Count;
            }
            catch (Exception e)
            {
                logger.LogEvent(e.StackTrace, 3);
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
