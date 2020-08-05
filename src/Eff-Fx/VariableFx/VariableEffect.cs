using Nessos.Effects;

namespace Eff_Fx.VariableFx
{
    /// <summary>
    /// Variable effect.
    /// </summary>
    public static class VariableEffect
    {
        public static CreateVariableEffect<TValue> CreateVariable<TValue>(string name, TValue value) => new CreateVariableEffect<TValue>(name, value);

        public static UpdateVariableEffect<TValue> UpdateVariable<TValue>(string name, TValue value) => new UpdateVariableEffect<TValue>(name, value);

        public static GetVariableEffect<TValue> GetVariable<TValue>(string name) => new GetVariableEffect<TValue>(name);

        public static ExistsVariableEffect GetVariable(string name) => new ExistsVariableEffect(name);

        public static DestroyVariableEffect DestroyVariable(string name) => new DestroyVariableEffect(name);
    }

    public abstract class VariableEffectBase<TResult> : Effect<TResult>
    {
        public VariableEffectBase(string name) => Name = name;

        public string Name { get; }
    }

    public class CreateVariableEffect<TValue> : VariableEffectBase<Unit>
    {
        public CreateVariableEffect(string name, TValue value) : base(name) { Value = value; }

        public TValue Value { get; }
    }

    public class UpdateVariableEffect<TValue> : VariableEffectBase<Unit>
    {
        public UpdateVariableEffect(string name, TValue value) : base(name) { Value = value; }

        public TValue Value { get; }
    }

    public class GetVariableEffect<TValue> : VariableEffectBase<TValue>
    {
        public GetVariableEffect(string name) : base(name) { }
    }

    public class ExistsVariableEffect : VariableEffectBase<bool>
    {
        public ExistsVariableEffect(string name) : base(name) { }
    }

    public class DestroyVariableEffect : VariableEffectBase<Unit>
    {
        public DestroyVariableEffect(string name) : base(name) { }
    }
}
