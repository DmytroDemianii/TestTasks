using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string SCORE = "Score: ";

    [SerializeField] private DestroyCubes _destroyer;
    [SerializeField] private UIController _UI;

    private int _score = 0;
    private float _scale;


    private void Awake()
    {
        _destroyer.OnCollected += CheckingForIncreaseStep;
        CheckingForIncreaseStep(0);
    }

    private void OnDestroy()
    {
        _destroyer.OnCollected -= CheckingForIncreaseStep;    
    }

    private void CheckingForIncreaseStep(int score)
    {
        this._score += score;
        _UI.UpdateCounter(_score);

        switch (this._score)
        {
            case 40:
                IncreaseOneStep(10);
                break;
            case 100:
                IncreaseOneStep(40);
                break;
            case 300:
                IncreaseOneStep(90);
                break;
            default:
            if (this._score >= 600)
            {
                InvokeWinPanel();
            }
                break;
        }
    }

    private void IncreaseOneStep(int value)
    {
        _destroyer._onStep += value;
        Debug.Log(_destroyer._onStep);
    }

    private void InvokeWinPanel()
    {

    }
}
