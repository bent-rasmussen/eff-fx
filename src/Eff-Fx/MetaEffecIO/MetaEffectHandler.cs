using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eff_Fx.MetaEffectIO
{
    public class MetaEffectHandler : EffectHandler
	{
		public MetaEffectHandler(params IEffectHandler[] handlers)
		{
			_handlerStack = new Stack<IEffectHandler>(handlers);
		}

		public MetaEffectHandler(IEnumerable<IEffectHandler> handlers)
		{
			_handlerStack = new Stack<IEffectHandler>(handlers);
		}

		private readonly Stack<IEffectHandler> _handlerStack;

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<Unit> { Effect: PushEffectHandlerEffect info } awtr:
					{
						_handlerStack.Push(info.Handler);
						awtr.SetResult(Unit.Value);
					}
					break;
				case EffectAwaiter<Unit> { Effect: PopEffectHandlerEffect info } awtr:
					{
						_ = _handlerStack.Pop();
						awtr.SetResult(Unit.Value);
					}
					break;
				default:
					foreach (var handler in _handlerStack)
					{
						handler.Handle(awaiter);
						if (awaiter.IsCompleted)
							break;
					}
					break;
			}
			return default;
		}
	}
}
