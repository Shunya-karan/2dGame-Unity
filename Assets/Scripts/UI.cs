using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount;

    private void Awake()
    {
        instance  = this;
        
    }
    private void Update()
    {
        timerText.text = Time.time.ToString("F2")+"s";
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }

}
