using Nessos.Effects;
using Nessos.Effects.Handlers;
using System;
using System.Threading.Tasks;

namespace Eff_Fx.ConsoleIO
{
    // Console Effect Handler

    public class ConsoleEffectHandler : EffectHandler
	{
		private string _indent = "";

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				case EffectAwaiter<string> { Effect: ReadLineEffect info } awtr:
					{
						var line = System.Console.ReadLine();
						awtr.SetResult(line);
						//awtr.SetResult($"#1: {line}");
						//awtr.SetResult($"#2: {line}");
					}
					break;
				case EffectAwaiter<Unit> { Effect: WriteLineEffect info } awtr:
					{
						System.Console.Write(_indent);
						System.Console.WriteLine(info.Line);
						awtr.SetResult(Unit.Value);
					}
					break;
				case EffectAwaiter<Unit> { Effect: IncrementIndentLineEffect info } awtr:
					{
						var newLength = _indent.Length + info.Spaces;
						if (newLength < 0)
							throw new InvalidOperationException("Cannot decrement indentation below zero.");
						_indent = new String(' ', newLength);
						awtr.SetResult(Unit.Value);
					}
					break;
			}
			return default;
		}
	}
}
