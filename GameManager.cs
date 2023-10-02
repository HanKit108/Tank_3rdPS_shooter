using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TankHealth player;
    [SerializeField] private GameObject gameOverText;

    void Start()
    {
        player.OnPlayerDied += TankHealth_OnPlayerDied;
    }

    private void TankHealth_OnPlayerDied(object sender, EventArgs e)
    {
        gameOverText.SetActive(true);
    }
}
