using System;
using Android.Telephony;
using Android.Content;
using TestDroid.Logic.Controller;
using System.Threading.Tasks;

namespace TestDroid
{
	public class Controller
	{
		private SMS sms;
        Context context;
        Logger logger;

		public Controller(Context cont)
		{
            context = cont;
            logger = Logger.GetInstance();
		}

        /// <summary>
        /// Sends an SMS
        /// </summary>
        /// <param name="text">The text (Body) of the SMS message</param>
        /// <param name="phoneNumber">The phone number to send the SMS to</param>
        /// <returns>True if the SMS is send</returns>
		public async Task<bool> SendSMS(string[] args)
		{
            sms = SMS.GetInstance(context);
            Task<bool> senderTask = sms.SendSMS(args);
            return await senderTask;
		}
	}
}
