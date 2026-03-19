using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 반드시 필요합니다.

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // UI용 TMP 텍스트 연결
    [SerializeField] private float remainingTime = 60f; // 시작 시간 (60초)
    [SerializeField] private GameObject GameOverObject; 

    private bool isTimerRunning = false;

    void Start()
    {
        // 게임 시작 시 타이머 바로 시작 (원할 때 시작하려면 호출 시점을 바꾸면 됩니다)
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                // 실시간으로 시간을 깎음
                remainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                // 시간이 0 이하가 되었을 때
                remainingTime = 0;
                isTimerRunning = false;
                GameOverObject.SetActive(true);
                UpdateTimerDisplay();
                OnTimerFinished();
            }
        }
    }

    // 시간을 화면에 예쁘게 표시해주는 함수
    void UpdateTimerDisplay()
    {
        // 초 단위만 표시 (소수점 버림)
        int seconds = Mathf.FloorToInt(remainingTime);
        timerText.text = seconds.ToString();
        
        // 만약 '0:59' 같은 분/초 형식을 원하시면 아래 줄 주석을 해제하세요.
        // int minutes = Mathf.FloorToInt(remainingTime / 60);
        // int secs = Mathf.FloorToInt(remainingTime % 60);
        // timerText.text = string.Format("{0:0}:{1:00}", minutes, secs);
    }

    void OnTimerFinished()
    {
        Debug.Log("시간 종료!");
        // 여기에 시간이 다 되었을 때 실행할 코드를 넣으세요.
    }
}