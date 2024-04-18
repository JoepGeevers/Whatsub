namespace Whatsub;

using System;

public class Whatsub
{
	private static List<ISubscription> subscriptions = [];

    public static void Subscribe<TMessage>(Action<TMessage> fn)
        where TMessage : notnull
            => Whatsub.subscriptions.Add(new Subscription<TMessage>(fn));

    public static void Publish<TMessage>(TMessage message)
		where TMessage : notnull
			=> Whatsub.subscriptions.ForEach(s => s.InvokeIf(message));

    internal static void Clear() => Whatsub.subscriptions = [];
}