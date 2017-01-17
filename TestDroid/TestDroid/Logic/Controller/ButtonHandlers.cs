using System;
using Android.Content;
using TestDroid.Logic.Controller;
using System.Threading.Tasks;

namespace TestDroid
{
	public class ButtonHandlers
	{
		Logger logger;
		Controller controller;
		Context context;

		public ButtonHandlers(Context context)
		{
			this.context = context;
			logger = Logger.GetInstance();
			controller = new Controller(context);
		}

		public void ButtonMakeCallHandler(object sender, EventArgs e)
		{
            string[] args = null;
			if (controller.MakeCall(args))
			{
				logger.LogEvent("Call succeeded", 0);
			}
		}

		public async void ButtonSendSmsHandler(object sender, EventArgs e)
		{
			string[] args = null;
			Task<bool> sendTask = controller.SendSMS(args);
			bool succes = await sendTask;
			if (succes)
			{
				logger.LogEvent("Send SMS succeeded", 0);
			}
		}

	}

}
