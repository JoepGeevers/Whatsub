namespace Whatsub
{
	internal interface ISubscription
	{
		void InvokeIf<TMessage>(TMessage message)
			where TMessage : notnull;
	}
}