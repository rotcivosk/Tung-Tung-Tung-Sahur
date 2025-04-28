using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI  scoreText;
    [SerializeField] private TextMeshProUGUI  timerText;
    [SerializeField] private GameObject gameOverPanel;

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

    private void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }
}
