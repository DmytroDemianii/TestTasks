using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	[SerializeField] GameObject explodePrefab;
	bool isExplode = false;


	private IEnumerator ExplodeCoroutine(GameObject explodePrefab, Vector3 position, float delayTime) 
	{
		GameObject explode = Instantiate(explodePrefab, position, Quaternion.identity, null);
		explode.transform.localScale =   transform.localScale;
		yield return new WaitForSeconds(delayTime);
		Destroy(explode);
		Debug.Log("explode");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			if (isExplode || !gameObject.activeSelf) 
			{
				return;
			}
			isExplode = true;
			
			StartCoroutine(ExplodeCoroutine(explodePrefab, transform.position, 0.75f));
			Destroy(other);
		}
	}
}