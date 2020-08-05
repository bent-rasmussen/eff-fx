using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Eff_Fx.ChannelIO
{
    public class ChannelEffectHandler<TMessage> : EffectHandler
	{
		public ChannelEffectHandler(Channel<TMessage> channel) => _channel = channel;

		private readonly Channel<TMessage> _channel;

		public override async ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<TMessage> { Effect: ReadChannelEffect<TMessage> info } awtr:
					var messageReceived = await _channel.Reader.ReadAsync();
					awtr.SetResult(messageReceived);
					break;
				case EffectAwaiter<Unit> { Effect: WriteChannelEffect<TMessage> info } awtr:
					await _channel.Writer.WriteAsync(info.Message);
					awtr.SetResult(Unit.Value);
					break;
				case EffectAwaiter<Unit> { Effect: CompleteChannelEffect<TMessage> info } awtr:
					_channel.Writer.Complete(info.Error);
					awtr.SetResult(Unit.Value);
					break;
			}
		}
	}
}
