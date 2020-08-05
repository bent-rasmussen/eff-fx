using Nessos.Effects;
using Nessos.Effects.Handlers;

namespace Eff_Fx.MetaEffectIO
{

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
