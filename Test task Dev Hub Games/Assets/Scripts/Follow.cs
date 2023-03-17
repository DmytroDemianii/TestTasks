using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float smooth = 5.0f;

    void Update()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y - 0.1f,
        target.transform.position.z + 2.59f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * smooth);
    }
}