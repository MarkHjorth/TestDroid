using Android.App;
using Android.Content;
using Android.Widget;

namespace TestDroid
{
	/// <summary>
	/// Recieve boot to handle what happens when phone is rebooted
	/// </summary>
	[BroadcastReceiver]
	[IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted },
		Categories = new[] { Android.Content.Intent.CategoryDefault })]
	public class ReceiveBoot : BroadcastReceiver
	{
		/// <summary>
		/// Overrides OnReceive for the boot intent - to start the app when phone boots.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="intent">Intent.</param>
		public override void OnReceive(Context context, Intent intent)
		{
			Toast.MakeText(context, "Starting TestDroid", ToastLength.Long).Show();

			if ((intent.Action != null) && (intent.Action == Android.Content.Intent.ActionBootCompleted))
			{ 	
				Android.Content.Intent start = new Android.Content.Intent(context, typeof(MainActivity));

				start.AddFlags(ActivityFlags.NewTask);
				context.ApplicationContext.StartActivity(start);
			}
		}
	}
}
