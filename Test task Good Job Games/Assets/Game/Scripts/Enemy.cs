using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	private Coroutine delayedDestroyCoroutine;


	private void OnDestroy()
	{
		GameManager.instance.enemies.Remove(this);
		GameManager.instance.pathLine.enemiesInRange.Remove(this);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			if (delayedDestroyCoroutine != null)
			{
				
				StopCoroutine(delayedDestroyCoroutine);
			}
			delayedDestroyCoroutine = StartCoroutine(DelayedDestroy(other.gameObject));
		}
	}

	private IEnumerator DelayedDestroy(GameObject obj)
	{
		yield return new WaitForSeconds(0.1f);
		if (obj != null)
			Destroy(obj);
		Destroy(gameObject);
	}
}
