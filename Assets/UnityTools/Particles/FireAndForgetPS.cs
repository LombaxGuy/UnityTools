using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireAndForgetPS : MonoBehaviour
{
    private ParticleSystem ps;
    private float lifeTime = 0;
    private float elapsedTime = 0;

    #region Awake & Start
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        lifeTime = ps.main.startLifetime.constant;
    }
    #endregion

    #region Updates
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
