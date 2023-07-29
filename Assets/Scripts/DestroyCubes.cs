using UnityEngine;
using UnityEngine.UI;
using System;

public class DestroyCubes : MonoBehaviour
{
    public event Action<int> OnCollected;

    public int _onStep;

    private void Start()
    {
        _onStep = 10;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            other.gameObject.SetActive(false);
            OnCollected?.Invoke(_onStep);
        }
    }
}
