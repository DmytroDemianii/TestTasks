using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private const string SCORE = "Score: ";

    [SerializeField] private Image _winPanel;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private Canvas _canvasWin;
    
    public void UpdateCounter(int number)
    {
        _counter.text = SCORE + number;
    }

    public void AppearWinPanel()
    {
        _canvasWin.enabled = true;
        _winPanel.DOFade(1, 2).OnComplete(() => _restartButton.SetActive(true));
    }

    // invoke from UI button 
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
