using Nessos.Effects;

namespace Eff_Fx.ConsoleIO
{
    // Console Effect
    public class WriteLineEffect : Effect<Unit>
	{
		public WriteLineEffect(string line) => Line = line;
		public string Line { get; }
	}

	public class ReadLineEffect : Effect<string>
	{
		public ReadLineEffect() { }
	}

	public class IncrementIndentLineEffect : Effect<Unit>
	{
		public IncrementIndentLineEffect(int spaces) => Spaces = spaces;
		public int Spaces { get; }
		//public int Tabs { get; }
	}
}
