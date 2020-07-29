# eff-fx
Experiments using the Nessos/Eff algebraic effects library.

## Ideas
Idea: use a source generator to create effects from an interface

Each concrete effect type is just capturing a single method, i.e.:

```csharp 
public class <MethodName>Effect
{
	// ...
	public <Type-Name> <Parameter1-Name> { get; }
	public <Type-Name> <Parameter2-Name> { get; }
	public <Type-Name> <ParameterN-Name> { get; }
}
```
