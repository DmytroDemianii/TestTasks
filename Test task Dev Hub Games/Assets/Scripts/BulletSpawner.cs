using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float shootDelay = 1.5f;
    [SerializeField] private GameObject FireBallPrefab;
    [SerializeField] private GameObject ElementalBallPrefab;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private Animator LeftHandAnimator;
    [SerializeField] private Animator RightHandAnimator;
    [SerializeField] private int speed;
    
    private float _LastShootTime;


    public void SpawnFireBall()
    {
        if (_LastShootTime + shootDelay < Time.time)
        {
            LeftHandAnimator.SetTrigger("AttackLeft");
            StartCoroutine(FireBallDelay());
            SpawnPoint.transform.position = new Vector3(-1.1f, -0.6f, 5.5f); 
            _LastShootTime = Time.time;
        }
    }

    public void SpawnElementalBall()
    {
        if (_LastShootTime + shootDelay < Time.time)
        {
            RightHandAnimator.SetTrigger("AttackRight");
            StartCoroutine(ElementalBallDelay());
            SpawnPoint.transform.position = new Vector3(1.1f, -0.6f, 5.5f); 
            _LastShootTime = Time.time;
        }
    }
    
    private IEnumerator ElementalBallDelay()
    {
        float animationLength = RightHandAnimator.GetCurrentAnimatorStateInfo(0).length;
        float animationTime = 0;

        while (animationTime < animationLength / 4)
        {
            animationTime += Time.deltaTime;
            yield return null;
        }

        GameObject bullet = Instantiate(ElementalBallPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = SpawnPoint.transform.forward * speed;
    }

    private IEnumerator FireBallDelay()
    {
        float animationLength = LeftHandAnimator.GetCurrentAnimatorStateInfo(0).length;
        float animationTime = 0;

        while (animationTime < animationLength / 4)
        {
            animationTime += Time.deltaTime;
            yield return null;
        }

        GameObject bullet = Instantiate(FireBallPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = SpawnPoint.transform.forward * speed;
    }
}
