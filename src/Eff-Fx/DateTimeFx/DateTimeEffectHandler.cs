using Nessos.Effects.Handlers;
using System;
using System.Threading.Tasks;

namespace Eff_Fx.TemporalFx
{
    public class DateTimeNowEffectHandler : EffectHandler
	{
		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			if (awaiter is EffectAwaiter<DateTime> { Effect: DateTimeNowEffect info } awtr)
				awtr.SetResult(DateTime.Now);
			return default;
		}
	}
}
