using Nessos.Effects;

namespace Eff_Fx.StateFx
{
    /// <summary>
    /// State effect.
    /// </summary>
    public static class StateEffect
    {
        public static GetStateEffect<TState> Get<TState>() => new GetStateEffect<TState>();

        public static PutStateEffect<TState> Put<TState>(TState state) => new PutStateEffect<TState>(state);
    }

    public class GetStateEffect<TState> : Effect<TState>
    {
    }

    public class PutStateEffect<TState> : Effect<Unit>
    {
        public PutStateEffect(TState state) => State = state;

        public TState State { get; }
    }
}
