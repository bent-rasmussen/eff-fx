using Nessos.Effects;

namespace Eff_Fx.ConsoleFx
{
    /// <summary>
    /// Console effect.
    /// </summary>
    public static class ConsoleIO
    {
        public static WriteLineEffect WriteLine(string line) => new WriteLineEffect(line);

        public static ReadLineEffect ReadLLine() => new ReadLineEffect();

        public static IncrementIndentLineEffect IncrementIndent(int spaces) => new IncrementIndentLineEffect(spaces);
    }

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
