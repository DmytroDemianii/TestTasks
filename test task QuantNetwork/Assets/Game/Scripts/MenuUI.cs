using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject lose;
    [SerializeField] private Animator animator;


    private void Start()
    {
        Animator animator = lose.GetComponent<Animator>();
    }

    public void Restart()
    {
        StartCoroutine(LoseAnimClose());
    }

    private IEnumerator LoseAnimClose()
    {
        animator.SetBool("isOpened", false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

    public void ManiMenu()
    {
        lose.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Game");
    }
    
	public void OnPlayClick()
    {
        SceneManager.LoadScene("Game");
	}

    public void LoseMenu()
    {
        animator.SetBool("isOpened", false);
        lose.SetActive(true);
    }
	public void Show()
    {
		menu.SetActive(true);
	}

	public void Hide()
	{
		menu.SetActive(false);
	}

    public void Quit()
    {
        Application.Quit();
    }
}
