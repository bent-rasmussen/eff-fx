using Nessos.Effects;
using Nessos.Effects.Handlers;
using System;
using System.Threading.Tasks;

namespace Eff_Fx.ProgressIO
{
    public class ProgressEffectHandler<TProgress> : EffectHandler
	{
		public ProgressEffectHandler(IProgress<TProgress> progress) => _progress = progress;

		private readonly IProgress<TProgress> _progress;

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			if (awaiter is EffectAwaiter<Unit> { Effect: ProgressEffect<TProgress> info } awtr)
			{
				_progress.Report(info.Value);
				awtr.SetResult(Unit.Value);
			}
			return default;
		}
	}
}
