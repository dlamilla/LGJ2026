using Unity.Cinemachine;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void GenerateShake()
    {
        impulseSource.GenerateImpulse();
    }
}
