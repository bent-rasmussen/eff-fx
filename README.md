# Eff Fx

Experiments using the Nessos/Eff algebraic effects library.

The code is in a state of flux and is currently mostly my developer notes/experiments.
Test cases and documentation is needed, as a result of this repository being in an
experimental phase.

## Open questions and design notes

### Composing effects and handlers

Composing effects and handlers is easy because a handler can just delegate to an internal list of handlers 
until the effect is handled. How about composing effect handlers as transformations of other effect handlers
though; obviously the realized effects such as writing to the console is not easily composed once it is 
executed, but what about having effect handlers act like transformations, e.g. transforming an effect of
one type into an effect of another type; or transforming an effect with one payload into another effect of
the same type but with transformed payload? e.g. composing a DateTimeEffecthandler with a 
ShiftedDateTimeEffectHandler where the shifted handler composes the plain handler but shifts the result into
the past or future.
