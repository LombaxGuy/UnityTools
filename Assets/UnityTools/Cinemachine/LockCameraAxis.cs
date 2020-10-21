using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraAxis : CinemachineExtension
{
    public bool lockX = false;
    [Conditional("lockX", ComparisonType.Equal, true)]
    public float xPosition = 0;

    public bool lockY = false;
    [Conditional("lockY", ComparisonType.Equal, true)]
    public float yPosition = 0;

    public bool lockZ = false;
    [Conditional("lockZ", ComparisonType.Equal, true)]
    public float zPosition = 0;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;

            if (lockX)
            {
                pos.x = xPosition;
            }

            if (lockY)
            {
                pos.y = yPosition;
            }

            if (lockZ)
            {
                pos.z = zPosition;
            }

            state.RawPosition = pos;
        }
    }
}