using Nessos.Effects;
using Nessos.Effects.Handlers;

namespace Eff_Fx.MetaEffectFx
{
    /// <summary>
    /// Meta effect. An effect that is intended to allow handlers to be composed in a stack.
    /// </summary>
    public static class MetaEffect
    {
        public static PushEffectHandlerEffect Push(IEffectHandler handler) => new PushEffectHandlerEffect(handler);

        public static PopEffectHandlerEffect Pop() => new PopEffectHandlerEffect();
    }

    public class PushEffectHandlerEffect : Effect<Unit>
	{
		public PushEffectHandlerEffect(IEffectHandler handler) { Handler = handler; }

		public IEffectHandler Handler { get; }
	}

	public class PopEffectHandlerEffect : Effect<Unit>
	{
		public PopEffectHandlerEffect() { }
	}
}
