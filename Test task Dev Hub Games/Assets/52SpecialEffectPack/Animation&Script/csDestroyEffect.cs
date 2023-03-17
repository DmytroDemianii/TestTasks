using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {

	void Update ()
    {

	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger find");
        Destroy(gameObject);
    }
}
