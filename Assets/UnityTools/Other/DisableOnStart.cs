using UnityEngine;

namespace UnityTools
{
    public class DisableOnStart : MonoBehaviour
    {
        #region Start & Awake
        private void Start()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}