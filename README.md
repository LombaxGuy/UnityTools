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

\- Cinemachine extensions \- <br>
LockCameraAxis for locking virtual cameras to specfic positions on the x, y and z axis. <br>

\- Unity extensions \- <br>
Vector3 extension for creating random directional vectors.

<B>Change log</B>

<B>\- 1.2 (In development)\-</B>
Added:
- Cinemachine extension for locking virtual cameras to specfic positions has been added. (requieres Cinemachine & my Conditional field attribute)
- Vector3 extension for creating a random directional vector.

<B>\- 1.1 (Latest release)\-</B>

Changed:
- The Conditional field attribute should now work for all types that Unity can serialize.
- CustomEvents now uses a generic RaiseEvent method for raising events with one parameter.

<B>\- 1.0 \-</B>

Initial release:
- Conditional and ReadOnly field attributes
- CustomEvents and example
- FireAndForget and ParticleEffect scripts
- DebugPoint, DebugRay and InGameDebugging console.
