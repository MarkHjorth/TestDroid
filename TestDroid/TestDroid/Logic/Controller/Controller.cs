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
		Call call;

		public Controller(Context context)
		{
            this.context = context;
            logger = Logger.GetInstance();
			call = new Call(context);
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

		public bool MakeCall(string[] args)
		{
			bool didSucceed = false;
			didSucceed = call.MakeCall(args);
			return didSucceed;
		}
	}
}
