using Nessos.Effects;
using System;

namespace Eff_Fx.ChannelIO
{
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
