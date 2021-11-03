using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    private Button button;
    private GameManager gameManager;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(call: SetDifficulty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    public void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
