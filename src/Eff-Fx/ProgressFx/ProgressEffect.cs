using Nessos.Effects;

namespace Eff_Fx.ProgressFx
{
    /// <summary>
    /// Progress effect.
    /// </summary>
    public static class ProgressEffect
    {
        public static ProgressReportEffect<TProgress> Report<TProgress>(TProgress progress, float? percentage = null) => 
            new ProgressReportEffect<TProgress>(progress, percentage);
    }

	public class ProgressReportEffect<TProgress> : Effect<Unit>
	{
		public ProgressReportEffect(TProgress value, float? percentage = null)
		{
			Value = value;
			Percentage = percentage;
		}

		public TProgress Value { get; }

		public float? Percentage { get; } // null => indefinite
	}
}
