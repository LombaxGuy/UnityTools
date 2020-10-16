using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    #region Start & Awake
    private void Start()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
