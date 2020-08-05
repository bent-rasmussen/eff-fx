using Nessos.Effects;

namespace Eff_Fx.StateIO
{
    public class GetStateEffect<TState> : Effect<TState>
	{
	}

	public class PutStateEffect<TState> : Effect<Unit>
	{
		public PutStateEffect(TState state) => State = state;
		public TState State { get; }
	}
}
