using Nessos.Effects;

namespace Eff_Fx.SequenceIO
{
    public class YieldSequenceItemEffect<TSource> : Effect<TSource>
	{
		public YieldSequenceItemEffect(TSource value) => Item = value;

		public TSource Item { get; }
	}
}
