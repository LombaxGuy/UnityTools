using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private bool keepRotation = false;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void Instantiate()
    {
        if (keepRotation)
        {
            Instantiate(particlePrefab, _transform.position, _transform.rotation);
        }
        else
        {
            Instantiate(particlePrefab, _transform.position, Quaternion.identity);
        }
    }
}
