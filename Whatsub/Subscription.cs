namespace Whatsub
{
	using System;

    internal record Subscription<TMessage>(Action<TMessage> fn) : ISubscription
    {
		private readonly Action<TMessage> fn = fn;

		public void InvokeIf<T>(T message)
			where T : notnull
		{
			if (typeof(T).Equals(typeof(TMessage)))
			{
				Task.Run(() => this.fn((TMessage)(object)message));
			}
		}
	}
}