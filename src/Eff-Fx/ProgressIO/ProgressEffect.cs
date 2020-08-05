using Nessos.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eff_Fx.ProgressIO
{

	public class ProgressEffect<T> : Effect<Unit>
	{
		public ProgressEffect(T value, float? percentage = null)
		{
			Value = value;
			Percentage = percentage;
		}
		public T Value { get; }
		public float? Percentage { get; } // null => indefinite
	}
}
