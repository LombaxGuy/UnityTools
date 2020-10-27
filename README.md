<B>Description</B>

Tools for Unity to help speed up development time.

<B>Contents</B>

\- Attributes \- <br>
Conditional field attribute. <br>
ReadOnly field attribute. <br>
Max field attribute. <br>

\- Events \- <br>
CustomEvents class & GameEvents example class. <br>

\- Particle systems \- <br>
FireAndForget script for particle system prefabs. <br>
ParticleEffect script for instantiating particle system prefabs. <br>

\- Debugging \- <br>
DebugPoint script & prefab. <br>
DebugRay struct. <br>
InGameDebug script & prefab for displaying log messages ingame. <br>

\- Cinemachine extensions \- <br>
LockCameraAxis for locking virtual cameras to specfic positions on the x, y and z axis. <br>

\- Unity extensions \- <br>
Vector3 extension for creating random directional vectors. <br>
SerializedProperty extension for getting the value of a property as an object. <br>

\- Other extensions \- <br>
Type extension for checking if a type is numeric. <br>

<B>Change log</B>

<B>\- 1.2 (Latest release)\-</B>

Added:
- Cinemachine extension for locking virtual cameras to specfic positions has been added. (requieres Cinemachine & my Conditional field attribute)
- Vector3 extension for creating a random directional vector.
- Max attribute has been added for setting upper limits to float and int values in the inspector.
- A SerializedProperty extension has been added for getting the value of a property as an object.
- A Type extension has been added for checking if a type is numeric.

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
