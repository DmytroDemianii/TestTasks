using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[SerializeField] Player player;
	[SerializeField] GameObject menu;
	[SerializeField] BackgroundMovement backgroundMovement;
	[SerializeField] MenuUI menuUI;
	[SerializeField] Spawner spawner;

	bool isPlaying = false;


	private void Awake()
	{
		instance = this;
		isPlaying = true;
		spawner.StartCoroutines(isPlaying);
	}

	void Update()
    {
		if(isPlaying == true)
		{
			backgroundMovement.MoveBackground(isPlaying);
		}	
    }

	public void StartGame()
	{
		new WaitForSeconds(1);
		isPlaying = true;
	}

	public void OnWin()
	{
		if(isPlaying == true)
		{
			Debug.Log("win");
			isPlaying = false;
			new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator LoanLoseMenu()
	{
		yield return new WaitForSeconds(1f);
		isPlaying = false;
		spawner.StartCoroutines(isPlaying);
		menuUI.LoseMenu();
	}

	public void OnLose() 
	{
		StartCoroutine(LoanLoseMenu());
	}
}
