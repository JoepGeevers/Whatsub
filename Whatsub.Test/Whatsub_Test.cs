namespace Whatsub.Test
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class Whatsub_Test
	{
        [TestInitialize]
        public void TestInitialize()
        {
			Whatsub.Clear();
        }

        [TestMethod]
		public void OnPublish_InvokeSubscription()
		{
			// arrange
			var created = 0;
			
			Whatsub.Subscribe<CarCreated>(m => created += m.CarId);

			// act
			Whatsub.Publish(new CarCreated(17));
						
			// assert
			Thread.Sleep(10);

			Assert.AreEqual(17, created);
		}

		[TestMethod]
		public void MessageGoesToCorrectSubscriber()
		{
			// arrange
			var created = 0;
			var deleted = 0;

			Whatsub.Subscribe<CarCreated>(m => created += m.CarId);
			Whatsub.Subscribe<CarDeleted>(m => deleted += m.CarId);

			// act
			Whatsub.Publish(new CarCreated(13));
			Whatsub.Publish(new CarDeleted(27));

			// assert
			Thread.Sleep(10);

			Assert.AreEqual(13, created);
			Assert.AreEqual(27, deleted);
		}

		[TestMethod]
		public void MessagesAreInTheirOwnThreads()
		{
			// arrange
			var cue = 0;

			Whatsub.Subscribe<CarCreated>(m =>
			{
				Thread.Sleep(100);
				cue = 1;
			});

			Whatsub.Subscribe<CarDeleted>(m => cue = 2);

			// act
			Whatsub.Publish(new CarCreated(13));
			Whatsub.Publish(new CarDeleted(27));

			// assert
			Thread.Sleep(200);

			Assert.AreEqual(1, cue);
		}

		public record CarCreated(int CarId) { }
		public record CarDeleted(int CarId) { }

		// todo: what about async?
		// todo: what about exceptions?
		// todo: what about async ANd exceptions?
	}
}