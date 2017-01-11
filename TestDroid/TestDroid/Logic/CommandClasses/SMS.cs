using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;
using TestDroid.Logic.Controller;
using System.Threading;
using System.Threading.Tasks;

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
		public async Task<bool> SendSMS(string[] args)
		{
            bool textSent = false;
            string text = "This text is sent by the TesDroid Application";
            string phoneNumber = "71840913";

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
                        logger.LogEvent(e.Message, 3);
                        throw;
                    }
                    break;
            }

            CancellationToken cancle = new CancellationTokenSource(2500).Token;

            Task<bool> checkIfSent = Task.Run(() => CountSms(cancle));

            try
			{
                logger.LogEvent("Sending SMS to: " + phoneNumber);
				smsManager.SendTextMessage(phoneNumber, null, text, null, null);
			}
			catch(Exception e)
			{
                logger.LogEvent(e.Message, 3);
                textSent = false;
			}

            textSent = await checkIfSent;
            
			return textSent;
		}
        
        private async Task<bool> CountSms(CancellationToken cancle)
        {
            bool textSent = false;
            uri = Telephony.Sms.Sent.ContentUri;
            cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);

            int before = cursor.Count;
            int after = cursor.Count;

            while (before == after)
            {
                cursor = context.ContentResolver.Query(uri, null, "read = 1", null, null);
                after = cursor.Count;
                if(cancle.IsCancellationRequested)
                {
                    logger.LogEvent("Text not sent. Timeout", 2);
                    return textSent;
                }
            }
            if (before != after)
            {
                logger.LogEvent("Text sent!");
                textSent = true;
            }
            return textSent;
        }
    }
}
