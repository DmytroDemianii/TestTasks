using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Navigator _navigator;
    [SerializeField] private HoleManager _holeManager;
    [SerializeField] private DestroyCubes _destroyer;
    [SerializeField] private UIController _UI;

    private int _score = 0;
    private float _scaleMultiplier = 1.5f;


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
                SetNewHoleScale();
                break;
            case 300:
                IncreaseOneStep(20);
                SetNewHoleScale();
                break;
            default:
            if (this._score >= 1000)
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

    private void SetNewHoleScale()
    {
        _holeManager.HoleUpScale(_scaleMultiplier);
    }

    private void InvokeWinPanel()
    {
        _navigator.MakeNonInteractable();
        _UI.AppearWinPanel();
        Debug.Log("WIN");
    }
}
