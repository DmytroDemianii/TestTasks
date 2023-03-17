using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 2f;
    public float leftBound = -10f;
    public Vector3 _startPosition;
    public static bool canFall;


    public void MoveBackground(bool isPlaying)
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x <= leftBound)
        {
            transform.position = _startPosition;
        }
    }
}