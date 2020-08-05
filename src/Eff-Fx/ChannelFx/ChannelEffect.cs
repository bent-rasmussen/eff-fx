using Nessos.Effects;
using System;

namespace Eff_Fx.ChannelFx
{
    /// <summary>
    /// Channel effect.
    /// </summary>
    public static class ChannelEffect
    {
        public static ReadChannelEffect<TMessage> Read<TMessage>() => new ReadChannelEffect<TMessage>();

        public static WriteChannelEffect<TMessage> Write<TMessage>(TMessage message) => new WriteChannelEffect<TMessage>(message);

        public static CompleteChannelEffect<TMessage> Complete<TMessage>() => new CompleteChannelEffect<TMessage>();
    }

    public class ReadChannelEffect<TMessage> : Effect<TMessage>
	{
		public ReadChannelEffect() { }
	}

	public class WriteChannelEffect<TMessage> : Effect<Unit>
	{
		public WriteChannelEffect(TMessage message) => Message = message;

		public TMessage Message { get; }
	}

	public class CompleteChannelEffect<TMessage> : Effect<Unit>
	{
		public CompleteChannelEffect(Exception? error = null) => Error = error;

		public Exception? Error { get; }
	}
}
