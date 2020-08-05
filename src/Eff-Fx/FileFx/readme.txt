
The design, as-is, allows IO "leaking" of resources, since streams can outlive effect handlers.

Remedies:

1) Gather streams in composite IDisposable and use the composite disposable in the handler.

2) Model streams as effects themselves, thus rendering them inert outside the scope of an effect handler.
