using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Threading.Tasks;

namespace Eff_Fx.StateFx
{
    public class StateEffectHandler<TState> : EffectHandler
	{
		public StateEffectHandler(TState initial) => _state = initial;

		private TState _state;

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<TState> { Effect: GetStateEffect<TState> info } awtr:
					awtr.SetResult(_state);
					break;
				case EffectAwaiter<Unit> { Effect: PutStateEffect<TState> info } awtr:
					_state = info.State;
					awtr.SetResult(Unit.Value);
					break;
			}
			return default;
		}
	}
}
