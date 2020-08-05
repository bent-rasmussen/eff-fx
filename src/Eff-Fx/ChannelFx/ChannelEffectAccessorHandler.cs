using Eff_Fx.Abstractions;
using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Eff_Fx.ChannelFx
{
    public class ChannelAccessor<TMessage> : IAccessor<TMessage>
    {
        public ChannelAccessor(Channel<TMessage> channel) => _channel = channel;

        private readonly Channel<TMessage> _channel;

        public ValueTask<TMessage> ReadAsync() => _channel.Reader.ReadAsync();

        public ValueTask WriteAsync(TMessage message) => _channel.Writer.WriteAsync(message);
    }

    public class ChannelEffectAccessorHandler<TMessage> : EffectHandler
    {
        public ChannelEffectAccessorHandler(IAccessor<TMessage> accessor) => _accessor = accessor;
        public ChannelEffectAccessorHandler(Channel<TMessage> channel) => _accessor = new ChannelAccessor<TMessage>(channel);

        private readonly IAccessor<TMessage> _accessor;
        private bool _isCompleted;

        public override async ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
        {
            switch (awaiter)
            {
                case EffectAwaiter<TMessage> { Effect: ReadChannelEffect<TMessage> info } awtr:
                    if (_isCompleted) throw new ChannelClosedException();
                    var messageReceived = await _accessor.ReadAsync();
                    awtr.SetResult(messageReceived);
                    break;
                case EffectAwaiter<Unit> { Effect: WriteChannelEffect<TMessage> info } awtr:
                    if (_isCompleted) throw new ChannelClosedException();
                    await _accessor.WriteAsync(info.Message);
                    awtr.SetResult(Unit.Value);
                    break;
                case EffectAwaiter<Unit> { Effect: CompleteChannelEffect<TMessage> info } awtr:
                    _isCompleted = true;
                    break;
            }
        }
    }
}
