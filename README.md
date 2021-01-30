<B>Description</B>

Tools for Unity to help speed up development time.

<B>Contents</B>

\- Events \- <br>
CustomEvents class & GameEvents example class. <br>

\- Particle systems \- <br>
FireAndForget script for particle system prefabs. <br>
ParticleEffect script for instantiating particle system prefabs. <br>

\- Debugging \- <br>
DebugPoint script & prefab. <br>
DebugRay struct. <br>
InGameDebug script & prefab for displaying log messages ingame. <br>

\- Tools \- <br>
Tool for numbering selected gameobjects. <br>
Build post processing script for automatically incrementing bundle id. <br>
Tool for measuring the bounding box of selected objects. <br>

\- Various extensions & Attributes \- <br>

<B>Change log</B>

<B>\- 1.4 (Latest release)\-</B>
Added:
- DebugRay now has an Empty, Equals and Draw methods to make it easier to use.
- An array extensions called contains has been added.
- A class containing string extensions has been added.
- A class containing int extensions has been added.
- A tool for measuring all selected objects bounding box has been added.

Changed:
- Numbering tool now saves settings in a Scriptable Object.
- AutoIncrementBundleVersion now saves settings in EditorPrefs.
- The GetLast and SetLast array extensions has been renamed to Last.
- Seperators has been added to the tool menu.

Bugfix:
- The checkmark on the menu item for AutoIncrementBundleVersion should now be displayed correctly.

<B>\- 1.3 \-</B>

Added:
- Tool for numbering multiple GameObjects at once.
- Build post processing script for automatically incrementing the bundle id when a new build is created.
- Array extensions for getting and setting the last element of the array.

Changed:
- All scripts are now contained in namespaces.

Bugfix:
- Moved a few scripts that caused build errors into "Editor" folders.

<B>\- 1.2 \-</B>

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
