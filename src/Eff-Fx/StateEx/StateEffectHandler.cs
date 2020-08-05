using Eff_Fx.Abstractions;
using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Threading.Tasks;

namespace Eff_Fx.StateFx
{
    public class StateAccessor<TState> : IAccessor<TState>
    {
        public StateAccessor(TState initial = default) => Value = initial;

        public TState Value { get; private set; }

        public ValueTask<TState> ReadAsync() => new ValueTask<TState>(Value);

        public ValueTask WriteAsync(TState message)
        {
            Value = message;
            return default;
        }
    }

    public class StateEffectHandler<TState> : EffectHandler
    {
        public StateEffectHandler(IAccessor<TState> initial) => State = initial;

        public IAccessor<TState> State { get; private set; }

        public override async ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<TState> { Effect: GetStateEffect<TState> info } awtr:
                    var x = await State.ReadAsync();
                    awtr.SetResult(x);
                    break;
				case EffectAwaiter<Unit> { Effect: PutStateEffect<TState> info } awtr:
					await State.WriteAsync(info.State);
					awtr.SetResult(Unit.Value);
					break;
			}
		}
	}
}
