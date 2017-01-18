using NUnit.Framework;
using TestDroidClient;
using System;
using System.Threading;

namespace TestDroidUnitTest
{
	[TestFixture()]
	public class Test
	{
		TestDroidClient.Controller controller = new TestDroidClient.Controller(null);

		[Test()]
		public void AUnknownCommand()
		{
			String[] command = {"ThisCommandDoesNotExcist"};

			Assert.False(controller.ParseCommand(command), "Something is terribly wrong. Check the code!");
		}

		[Test()]
		public void BFlightModeOn()
		{
			String[] command = { "flightmode", "1" };

			Assert.True(controller.ParseCommand(command), "Could not turn flightmode on!");
		}

		[Test()]
		public void CFlightModeOff()
		{
			String[] command = { "flightmode", "0" };

			Assert.True(controller.ParseCommand(command), "Could not turn flightmode off!");
		}

		[Test()]
		public void DCallDial()
		{
			String[] command = { "call", "dial" };

			Assert.True(controller.ParseCommand(command), "Could not make call!");
			Thread.Sleep(3000);
		}

		[Test()]
		public void ECallEnd()
		{
			String[] command = { "call", "end" };

			Assert.True(controller.ParseCommand(command), "Could not end call!");
		}

	}
}
