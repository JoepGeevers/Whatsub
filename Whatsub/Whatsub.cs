namespace Whatsub;

using System;

public class Whatsub
{
	private static readonly List<ISubscription> subscriptions = [];

    public static void Subscribe<TMessage>(Action<TMessage> fn)
        where TMessage : notnull
            => subscriptions.Add(new Subscription<TMessage>(fn));

    public static void Publish<TMessage>(TMessage message)
		where TMessage : notnull
			=> subscriptions.ForEach(s => s.InvokeIf(message));
}