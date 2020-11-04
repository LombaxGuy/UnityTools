using TMPro;
using UnityEngine;

namespace UnityTools.Debugging
{
    public class InGameDebug : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logText;
        [SerializeField] private TextMeshProUGUI cameraPos;

        private void OnEnable()
        {
            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }

        private void Update()
        {
            if (Camera.main != null)
            {
                cameraPos.text = "CameraPos: " + Camera.main.transform.position.ToString();
            }
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }

        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            logText.text += logType + ": " + logMessage + "\n";

            if (logType == LogType.Exception || logType == LogType.Error)
            {
                logText.text += stackTrace + "\n";
            }
        }
    }
}