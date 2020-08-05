using Nessos.Effects;
using Nessos.Effects.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eff_Fx.SequenceFx
{
    public class SequenceEffectHandler<TSource> : EffectHandler
	{
		public SequenceEffectHandler() => _sequence = new List<TSource>();

		private readonly List<TSource> _sequence;

		public IReadOnlyCollection<TSource> Items => _sequence.AsReadOnly();

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<Unit> { Effect: YieldSequenceItemEffect<TSource> info } awtr:
					_sequence.Add(info.Item);
					awtr.SetResult(Unit.Value);
					break;
			}
			return default;
		}
	}
}
