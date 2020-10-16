<B>Description</B>

Tools for Unity to help speed up development time.

<B>Contents</B>

\- Attributes \- <br>
Conditional field attribute <br>
ReadOnly field attribute <br>

\- Events \- <br>
CustomEvents class & GameEvents example class <br>

\- Particle systems \- <br>
FireAndForget script for particle system prefabs <br>
ParticleEffect script for instantiating particle system prefabs <br>

\- Debugging \- <br>
DebugPoint script & prefab <br>
DebugRay struct <br>
InGameDebug script & prefab for displaying log messages ingame <br>


<B>Change log</B>

<B>\- 1.1 \-</B>

Changed:
- The Conditional field attribute should now work for all types that Unity can serialize.
- CustomEvents now uses a generic RaiseEvent method for raising events with one parameter.

<B>\- 1.0 \-</B>

Initial release:
- Conditional and ReadOnly field attributes
- CustomEvents and example
- FireAndForget and ParticleEffect scripts
- DebugPoint, DebugRay and InGameDebugging console.
