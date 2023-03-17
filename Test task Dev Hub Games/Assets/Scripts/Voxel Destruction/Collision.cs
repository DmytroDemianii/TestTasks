using System.Collections;
using UnityEngine;

public class Collision : MonoBehaviour
{
	private Collider[] hitColliders;

	public float blastRadius;
	public float explosionPower;
	public LayerMask explosionLayers;

	public GameObject Particles;
	
	
	private void Start()
	{
		StartCoroutine(DestroyParticles());
	}

	private void OnCollisionEnter(UnityEngine.Collision col)
	{
		Particles.gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
		Instantiate(Particles, col.transform.position, Quaternion.identity);

		destroy(col.contacts[0].point);
		Destroy(gameObject, 0.2f);
		if(col.gameObject.tag == "floor")
		{
			Destroy(gameObject, 3);
		}
	}

	IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject, 5);
    }

	void destroy(Vector3 explosionPoint)
	{
		hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
		foreach (Collider hitCol in hitColliders)
		{
			if (hitCol.GetComponent<Rigidbody>() == null)
			{
				hitCol.GetComponent<MeshRenderer>().enabled = true;
				hitCol.gameObject.AddComponent<Rigidbody>();

				hitCol.GetComponent<Rigidbody>().mass = 500;
				hitCol.GetComponent<Rigidbody>().isKinematic = false;

				hitCol.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 5;
				hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
			}
		}
	}
}
