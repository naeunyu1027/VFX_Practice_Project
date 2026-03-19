using UnityEngine;

public class PingPongMove : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f; // 이동 속도
    [SerializeField] private float minX = -11.0f; // 왼쪽 끝 지점
    [SerializeField] private float maxX = 11.0f;  // 오른쪽 끝 지점

    private int direction = -1; // 현재 이동 방향 (-1: 왼쪽, 1: 오른쪽)
    [SerializeField] private GameObject GameOverObject; 
    void Update()
    {
        // 1. 현재 방향으로 이동
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // 2. 왼쪽 끝(-11)에 도달하면 방향을 오른쪽(1)으로 변경
        if (transform.position.x <= minX)
        {
            direction = 1;
            // 위치 보정 (범위를 벗어나지 않게)
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        // 3. 오른쪽 끝(11)에 도달하면 방향을 왼쪽(-1)으로 변경
        else if (transform.position.x >= maxX)
        {
            direction = -1;
            // 위치 보정
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }
}