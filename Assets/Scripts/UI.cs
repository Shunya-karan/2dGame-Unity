using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUi;
    [Space]
    public static UI instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;

    }

    private void Update()
    {
        timerText.text = Time.time.ToString("F2") + "s";
    }

    public void EnableGameOverUi()
    {
        Time.timeScale = .5f;
        gameOverUi.SetActive(true);
    }

    public void RestartLevel()
    {

    Debug.Log("RESTART CLICKED");

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("Loading scene: " + sceneIndex);

        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }

}
