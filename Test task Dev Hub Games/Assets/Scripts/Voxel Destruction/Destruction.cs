using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Destruction : MonoBehaviour
{
	[SerializeField] private GameObject mesh;

	private float cubeWidth;
	private float cubeHeight;
	private float cubeDepth;

	public float cubeScale = 0.3f;


	void Start()
	{
		cubeWidth = transform.localScale.z;
		cubeHeight = transform.localScale.y;
		cubeDepth = transform.localScale.x;
		mesh.gameObject.GetComponent<Transform>().localScale = new Vector3(cubeScale, cubeScale, cubeScale);
	}

	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{ 
			CreateCube();
			GetComponent<BoxCollider>().isTrigger = true;
			Destroy(gameObject, 3);
		}
		if (collision.gameObject.tag == "floor")
		{
			Destroy(gameObject, 3);
		} 
	}

	void CreateCube()
	{
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
		this.gameObject.GetComponent<BoxCollider>().isTrigger = true;

		if (gameObject.CompareTag("box"))
		{
			for (float x = 0; x < cubeWidth; x += cubeScale)
			{
				for (float y = 0; y < cubeHeight; y += cubeScale)
				{
					for (float z = 0; z < cubeDepth; z += cubeScale)
					{
						Vector3 vec = transform.position;

						GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), Quaternion.identity);
						cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
					}
				}
			}
		}
	}
}
