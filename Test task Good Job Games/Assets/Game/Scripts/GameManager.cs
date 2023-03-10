using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	const string LEVEL_KEY = "GameManager.currLevel";
	public static GameManager instance = null;

	public PathLine pathLine;

	[System.NonSerialized] public int currLevel = 0;
	[System.NonSerialized] public List<Enemy> enemies = new List<Enemy>();
	[SerializeField] LineRenderer shootLine;
	[SerializeField] LayerMask hitMask;
	[SerializeField] Player player;
	[SerializeField] GameObject menu;
	[SerializeField] Transform[] jumpPos;

	bool isPlaying = false;
	private bool isHolding = false;

	float holdTime = 0;
	Vector3 lastTouchPos;

	private void Awake()
	{
		currLevel = PlayerPrefs.GetInt(LEVEL_KEY, 1);
		shootLine.gameObject.SetActive(false);
		instance = this;
	}

	void Update()
    {
        if (!isPlaying || !player.isCanShoot)
            return;

        if (pathLine.EnemiesCount == 0)
        {
            OnWin();
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootLine.gameObject.SetActive(true);
            holdTime = 0.0f;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f, hitMask))
            {
                lastTouchPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
                pathLine.SetWidth(player.GetPathWidth());
                shootLine.startWidth = shootLine.endWidth = player.GetBulletWidth();
            }
            player.InitShoot();
        }

        if (Input.GetMouseButton(0))
        {
            if (!isHolding)
            {
                isHolding = true;
            }

            holdTime += Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f, hitMask))
            {
                lastTouchPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
                shootLine.SetPosition(1, lastTouchPos);
                pathLine.SetWidth(player.GetPathWidth());
                shootLine.startWidth = shootLine.endWidth = player.GetBulletWidth();
            }

            player.OnHold(lastTouchPos, holdTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
            shootLine.gameObject.SetActive(false);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f, hitMask))
            {
                Vector3 shootTo = new Vector3(hit.point.x, 0.01f, hit.point.z);
                player.ShootTo(shootTo, holdTime);
            }
        }
    }

	public void StartGame()
	{
		StartCoroutine(StartGameCoroutine());
	}
	private IEnumerator StartGameCoroutine()
	{
		yield return new WaitForSeconds(0.2f);
		isPlaying = true;
	}

	public void OnWin()
	{
		if(isPlaying == true)
		{
			Debug.Log("win");
			isPlaying = false;
			shootLine.gameObject.SetActive(false);
			player.ShootTo(lastTouchPos, holdTime);
			PlayerPrefs.SetInt(LEVEL_KEY, ++currLevel);
			new WaitForSeconds(0.5f);
			StartCoroutine(JumpRoutine());
			StartCoroutine(ReloadLevelRoutine());
		}
	}

	private IEnumerator JumpRoutine()
	{
		for(int i = 1; i < jumpPos.Length; ++i)
		{
			float time = 0f;
			while(time < 1.0f)
			{
				time += Time.deltaTime / 1.0f;
				Vector3 newPos = Vector3.Lerp(jumpPos[i - 1].position, jumpPos[i].position, time);
				if(time < 0.5f)
					newPos.y = Mathf.Lerp(jumpPos[i - 1].position.y, jumpPos[i].position.y + 3.0f, time * 2f);
				else
					newPos.y = Mathf.Lerp(jumpPos[i].position.y + 3.0f, jumpPos[i].position.y, (time - 0.5f) * 2f);
				newPos.y = Mathf.Sqrt(newPos.y);
				player.transform.position = newPos;
				yield return null;
			}
		}
	}

	private IEnumerator ReloadLevelRoutine()
	{
		yield return new WaitForSeconds(4.6f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnLose() 
	{
		isPlaying = false;
		Application.LoadLevel(Application.loadedLevel);
	}
}
