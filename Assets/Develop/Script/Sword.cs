using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float throwSpeed = 60.0f;
    
    // Clear 시 활성화할 오브젝트 (인스펙터에서 드래그 앤 드롭)
    [SerializeField] private GameObject clearObject; 

    private float currentY;
    private bool isFlying = false;
    private Vector3 flyDirection;

    // 리셋될 위치와 회전값 설정
    private readonly Vector3 resetPosition = new Vector3(0f, 0f, 23.17f);

    void Start()
    {
        // 시작 시 초기 위치와 각도 세팅
        ResetSwordState();
        
        // 시작할 때 Clear 오브젝트는 꺼둠 (선택 사항)
        if (clearObject != null) clearObject.SetActive(false);
    }

    void Update()
    {
        // Spacebar 입력 시
        if (Input.GetKeyDown(KeyCode.Space) && !isFlying)
        {
            Score sm = FindObjectOfType<Score>();
            
            if (sm != null)
            {
                if (sm.AddThrowCount())
                {
                    isFlying = true;
                    flyDirection = -transform.forward; 
                }
            }
        }

        if (isFlying)
        {
            transform.position += flyDirection * throwSpeed * Time.deltaTime;
        }
        else
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A)) currentY -= rotationSpeed;
        else if (Input.GetKey(KeyCode.D)) currentY += rotationSpeed;

        currentY = Mathf.Clamp(currentY, -90f, 90f);
        transform.localRotation = Quaternion.Euler(0, currentY, 0);
    }

    // --- 충돌 감지 로직 ---
    private void OnTriggerEnter(Collider other)
    {
        if (isFlying)
        {
            // 1. 만약 충돌한 대상의 태그가 "Enemy"라면
            if (other.CompareTag("Finish"))
            {
                // Clear 오브젝트 활성화
                if (clearObject != null)
                {
                    clearObject.SetActive(true);
                }
                Debug.Log("Enemy 충돌! Clear!");
            }

            // 2. 어떤 것이든 부딪히면 일단 리셋
            ResetSwordState();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFlying)
        {
            // 물리 충돌 시에도 태그 체크
            if (collision.gameObject.CompareTag("Finish"))
            {
                if (clearObject != null) clearObject.SetActive(true);
            }

            ResetSwordState();
        }
    }

    private void ResetSwordState()
    {
        isFlying = false;
        transform.position = resetPosition;
        currentY = 0f;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}   