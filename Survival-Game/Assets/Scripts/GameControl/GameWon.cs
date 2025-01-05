using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWon : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gameFinishedText;

    [SerializeField] private GameObject gameWonCheckPoint;

    void Start()
    {
        gameFinishedText.gameObject.SetActive(false);
        gameWonCheckPoint.SetActive(false);
    }

    void GameFinished()
    {
        if (gameFinishedText != null && gameWonCheckPoint != null)
        {
            gameFinishedText.gameObject.SetActive(true);
            gameWonCheckPoint.SetActive(true);
        }




    }
}
