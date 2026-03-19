using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private int maxThrows = 10;
    
    private int currentThrows = 0;
    private bool isGameOver = false;
        [SerializeField] private GameObject GameOverObject; 


    void Start()
    {
        UpdateUI();
    }

    // 검 스크립트에서 호출할 함수
    public bool AddThrowCount()
    {
        if (isGameOver) return false; // 이미 게임오버면 실행 안 함

        currentThrows++;
        UpdateUI();

        if (currentThrows >= maxThrows)
        {
            TriggerGameOver();
        }
        return true;
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{maxThrows} / {currentThrows}";
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("게임 오버! 10번을 모두 사용했습니다.");
        GameOverObject.SetActive(true);
        Time.timeScale = 0; // 필요시 게임을 멈추려면 주석 해제
    }

    public bool IsGameOver() => isGameOver;
}
