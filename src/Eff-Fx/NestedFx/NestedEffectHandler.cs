using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nessos.Effects.Handlers;

namespace Eff_Fx.NestedFx
{
    /// <summary>
    /// Effect handler that handles an effect by delegating to a list of effects,
    /// one at a time, until the effect is handled, or it completes unhandled.
    /// </summary>
    public class NestedEffectHandler : EffectHandler
    {
        public NestedEffectHandler(params IEffectHandler[] handlers)
        {
            _handlers = new List<IEffectHandler>(handlers);
        }

        public NestedEffectHandler(IEnumerable<IEffectHandler> handlers)
        {
            _handlers = handlers.ToList();
        }

        private readonly List<IEffectHandler> _handlers;

        public override async ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
        {
            if (awaiter is EffectAwaiter<IEffectHandler> { Effect: GetCurrentHandlerEffect _ } awtr)
            {
                awtr.SetResult(this);
            }
            else
            {
                foreach (var handler in _handlers)
                {
                    await handler.Handle(awaiter);
                    if (awaiter.IsCompleted)
                        break;
                }
            }
        }
    }
}
