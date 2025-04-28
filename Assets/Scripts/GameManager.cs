using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private int score = 0;
    private float timer = 30f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (isGameOver) return;

        timer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            GameOver();
        }
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Freeze the game
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + score.ToString();
    }

    public void Retry()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Retrying the game...");
    }
}