using Nessos.Effects;
using Nessos.Effects.Handlers;
using System;
using System.Threading.Tasks;

namespace Eff_Fx.TemporalFx
{
    /// <summary>
    /// DateTime effect.
    /// </summary>
    public static class DateTimeEffect
    {
        public static DateTimeNowEffect Now => new DateTimeNowEffect();
    }

    public class DateTimeNowEffect : Effect<DateTime> { }
}
