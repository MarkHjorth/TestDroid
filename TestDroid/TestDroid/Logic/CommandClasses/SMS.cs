using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;
using TestDroid.Logic.Controller;
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
        
        Logger logger;

        private SMS(Context cont)
		{
			smsManager = SmsManager.Default;
            context = cont;
            logger = Logger.GetInstance();
		}

        public bool TextSent { get; set; }

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
		public bool SendSMS(string[] args)
		{
            string text = "Harambe did 9/11";
            string phoneNumber = "41618934";

            switch (args.Length)
            {
                case 2:
                    break;
                case 3:
                    text = args[2];
                    break;
                case 4:
                    text = args[2];
                    phoneNumber = args[3];
                    break;
                default:
                    try
                    {
                        text = args[2];
                        phoneNumber = args[3];
                    }
                    catch (Exception e)
                    {
                        logger.LogEvent(e.StackTrace, 3);
                        throw;
                    }
                    break;
            }

            int countBefore = 0;

            try
            {
                uri = Telephony.Sms.ContentUri;
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
                TextSent = true;
			}
			catch(Exception e)
			{
                logger.LogEvent(e.StackTrace, 3);
                TextSent = false;
			}

            Thread t = new Thread(new ThreadStart(() => CountSms(countBefore)));
            t.Start();
            t.Join();
			return TextSent;
		}
        
        private void CountSms(int before)
        {
            int after = 0;
            uri = Telephony.Sms.ContentUri;
            cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
            before = cursor.Count;

            Thread.Sleep(2000);

            uri = Telephony.Sms.ContentUri;
            cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
            after = cursor.Count;
            if (before != after)
            {
                logger.LogEvent("Text sent!");
                TextSent = true;
            }
            else
            {
                logger.LogEvent("Text not sent");
                TextSent = false;
            }
        }
    }
}
