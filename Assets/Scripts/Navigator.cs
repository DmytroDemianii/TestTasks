using System;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] public float _moveSpeed;

    [Header("Gravity")] 
    
    [Range(1f, 1000f)]public float power = 1f; // gravity power
    [Range(-10f, 10f)] public float upOrDown; // direction of gravity
    [Range(1f, 20f)]public float forceRange = 1f; // range of gravity
    
    public LayerMask layerMask; // determines which layer should be affected by gravity

    private void FixedUpdate()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveAmount = moveDirection * _moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + moveAmount;
        transform.position = newPosition;

        Gravity(transform.position,forceRange,layerMask);
    }

    private void Gravity(Vector3 gravitySource, float range, LayerMask layerMask)
    {
        Collider[] objs = Physics.OverlapSphere(gravitySource, range, layerMask);

        for (int i = 0; i < objs.Length; i++)
        {
            Rigidbody rbs = objs[i].GetComponent<Rigidbody>();
            
            Vector3 forceDirection = new Vector3(gravitySource.x,upOrDown,gravitySource.z) - objs[i].transform.position;
            
            rbs.AddForceAtPosition(power * forceDirection.normalized,gravitySource);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,forceRange);
    }
}

