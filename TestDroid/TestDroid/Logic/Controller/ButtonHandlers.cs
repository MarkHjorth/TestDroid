using System;
using TestDroid.Logic.Controller;
namespace TestDroid
{
	public class ButtonHandlers
	{
		Logger logger;

		public ButtonHandlers()
		{
			logger = Logger.GetInstance();
		}

		public void ButtonMakeCallHandler(object sender, EventArgs e)
		{
			logger.LogEvent("jada");
		}

	}

}
