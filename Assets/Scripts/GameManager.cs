using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public List<GameObject> targets;
    public GameObject gameOverPanel;
    public GameObject menuPanel;
    public GameObject pausePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private bool isgamePaused = false;
    private float spawnRate = 1.0f;
    private int index = 0, score = 0, lives = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }        
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        menuPanel.SetActive(false);
        livesText.text = "Lives: " + lives;
        isGameActive = true;
        StartCoroutine(nameof(SpawnTarget));
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseLives(int livesToChange)
    {
            lives += livesToChange;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
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

    private void PauseGame()
    {
        if (!isgamePaused)
        {
            isgamePaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        isgamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
