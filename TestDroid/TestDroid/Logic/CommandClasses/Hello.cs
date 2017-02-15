using System;
using Android.Telephony;
using Android.Provider;
using Android.Database;
using Android.Content;
using TestDroid.Logic.Controller;
using System.Threading;
using System.Threading.Tasks;
using Android.Widget;

namespace TestDroid
{
	public class Hello
	{
		private static Hello instance;
		private static Context context;

		Logger logger;

		private Hello(Context cont)
		{
			context = cont;
			logger = Logger.GetInstance();
		}

		/// <summary>
		/// Calls and returns the only possible instance of Hello
		/// </summary>
		/// <param name="c">The Context</param>
		/// <returns></returns>
		public static Hello GetInstance(Context context)
		{
			if (instance == null)
			{
				instance = new Hello(context);
			}
			return instance;
		}

		public async Task<bool> SayHello(string[] args)
		{
			bool didSucceed = false;

			string logOne = "Client says hello!";
			string logTwo = "Saying hello back...";

			logger.LogEvent(logOne, 0);
			logger.LogEvent(logTwo, 0);

			didSucceed = true;

			return didSucceed;
		}
	}
}
