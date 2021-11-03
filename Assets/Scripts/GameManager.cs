using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public List<GameObject> targets;
    public Button restartButton;
    public GameObject menuPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    private float spawnRate = 1.0f;
    private int index = 0, score = 0;

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        menuPanel.SetActive(false);
        isGameActive = true;
        StartCoroutine(nameof(SpawnTarget));
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
