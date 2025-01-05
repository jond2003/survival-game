using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWon : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gameFinishedText;

    [SerializeField] private GameObject gameWonCheckPoint;

    public static GameWon Instance { get; private set; }

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        gameFinishedText.gameObject.SetActive(false);
        gameWonCheckPoint.SetActive(false);
    }

    public void GameFinished()
    {
        if (gameFinishedText != null && gameWonCheckPoint != null)
        {
            gameFinishedText.gameObject.SetActive(true);
            gameWonCheckPoint.SetActive(true);
        }
    }
}
