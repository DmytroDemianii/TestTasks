using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMovement : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float speed;

    private float LeftBorder = -26f;


    private void FixedUpdate() 
    {
        enemy.transform.Translate(-speed * Time.deltaTime,0,0);
        
        if (transform.position.x <= LeftBorder)
        {
            Destroy(enemy);
        }
    }
}
