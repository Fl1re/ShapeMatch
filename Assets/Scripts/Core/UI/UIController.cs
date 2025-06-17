using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button respawnButton;
    [SerializeField] private BoardSpawner boardSpawner;
    [SerializeField] private ActionBar actionBar;

    private int matchCount = 0;
    private int matchesToWin;

    public UnityEvent OnWin;
    public UnityEvent OnLose;


    private void Awake()
    {
        boardSpawner.OnSpawningStateChanged += HandleSpawningState;
        matchesToWin = boardSpawner.InitialFigureCount / 3;
    }

    private void OnDestroy()
    {
        boardSpawner.OnSpawningStateChanged -= HandleSpawningState;
    }

    private void OnEnable()
    {
        ShapeSpawnerEvents.OnMatch += OnMatch;
        actionBar.OnLose.AddListener(HandleLose);
    }

    private void OnDisable()
    {
        ShapeSpawnerEvents.OnMatch -= OnMatch;
        actionBar.OnLose.RemoveListener(HandleLose);
    }

    private void HandleSpawningState(bool isSpawning)
    {
        respawnButton.interactable = !isSpawning;
    }
    
    private void OnMatch()
    {
        matchCount++;
        if (matchCount >= matchesToWin)
        {
            OnWin?.Invoke();
        }
    }
    
    public void ResetLevel()
    {
        matchCount = 0;
        actionBar.Clear();
        boardSpawner.RespawnBoard(true);
    }

    private void HandleLose()
    {
        OnLose?.Invoke();
    }
}
