using UnityEngine;
using System;

public class DestroyCubes : MonoBehaviour
{
    [SerializeField] private AudioSource _bubbleSound;
    public event Action<int> OnCollected;

    public int _onStep { get; set;}

    private void Start()
    {
        _onStep = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            _bubbleSound.PlayOneShot(_bubbleSound.clip);
            other.gameObject.SetActive(false);
            OnCollected?.Invoke(_onStep);
        }
    }
}
