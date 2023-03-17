using UnityEngine;

public class InputSystem : MonoBehaviour
{	
	[SerializeField] BulletSpawner bulletSpawner;
	public float minSwipeDistance = 50f;
	public float maxSwipeTime = 1f;
	private Vector2 touchStart;
	private float touchStartTime;
    public static bool isPlaying = true;


	void Update()
	{
        if(isPlaying == true)
        {
		    InputDetection();
        }
	}

	void InputDetection()
	{	
		if (Input.touchCount > 0)
		{
        Touch touch = Input.GetTouch(1);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStart = touch.position;
                touchStartTime = Time.time;
                break;

            case TouchPhase.Ended:
                Vector2 touchEnd = touch.position;
                float touchEndTime = Time.time;
                float swipeDistance = Vector2.Distance(touchEnd, touchStart);
                float swipeTime = touchEndTime - touchStartTime;

                if (swipeDistance >= minSwipeDistance && swipeTime <= maxSwipeTime)
                {
                    if (Mathf.Abs(touchEnd.y - touchStart.y) > Mathf.Abs(touchEnd.x - touchStart.x))
                    {
                        if (touchEnd.y < touchStart.y)
                        {
                            if (touchStart.x > Screen.width / 2f)
                            {
                                Debug.Log("Swipe down");
                                bulletSpawner.SpawnFireBall();
                            }
                        }
						if (touchEnd.y > touchStart.y)
						{
							if (touchStart.x > Screen.width / 2f)
                            {
                                Debug.Log("Swipe up");
                                bulletSpawner.SpawnElementalBall();
                            }
						}
                    }
                }
                
            break;
        }
    }
}
}
