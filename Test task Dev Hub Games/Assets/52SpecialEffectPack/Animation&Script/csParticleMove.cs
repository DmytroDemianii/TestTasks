using UnityEngine;

public class csParticleMove : MonoBehaviour
{
    public float speed = 10f;

	void Update ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
