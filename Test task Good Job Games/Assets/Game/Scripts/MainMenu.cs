using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
	[SerializeField] TextMeshProUGUI levelTextField = null;
	[SerializeField] GameObject menu;

	private void Start() {
//		levelTextField.text = $"Level: {GameManager.instance.currLevel}";
	}

	public void OnPlayClick() {
		Hide();
		GameManager.instance.StartGame();
	}

	public void Show() {
		levelTextField.text = $"Level: {GameManager.instance.currLevel}";
		menu.SetActive(true);
	}

	public void Hide()
	{
		menu.SetActive(false);
	}
}
