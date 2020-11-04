using UnityEngine;
using UnityEditor;

namespace UnityTools.Debugging
{
    public class DebugPoint : MonoBehaviour
    {
        [Header("Lable")]
        public string text = "";

        [Header("Point")]
        public Color pointColor = Color.green;
        public float size = 0.2f;

        private void OnDrawGizmos()
        {
#if DEBUG
            // Draw handles
            if (text != "")
                Handles.Label(transform.position, text);

            // Draw gizmos
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(transform.position, size);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
#endif
        }
    }
}