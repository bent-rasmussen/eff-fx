using Nessos.Effects;

namespace Eff_Fx.SequenceFx
{
    /// <summary>
    /// Sequence effect.
    /// </summary>
    public static class SequenceEffect
    {
        public static YieldSequenceItemEffect<TSource> Yield<TSource>(TSource item) =>
            new YieldSequenceItemEffect<TSource>(item);
    }

    public class YieldSequenceItemEffect<TSource> : Effect<TSource>
	{
		public YieldSequenceItemEffect(TSource value) => Item = value;

		public TSource Item { get; }
	}
}
