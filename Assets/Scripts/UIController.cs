using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private const string SCORE = "Score: ";

    [SerializeField] private TextMeshProUGUI _counter;

    public void UpdateCounter(int score)
    {
        _counter.text = SCORE + score;
    }
}
