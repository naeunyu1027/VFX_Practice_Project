using UnityEngine;

public class Player : MonoBehaviour
{
    

    [Header("이동 설정")]
    public float movementSpeed = 10.0f; 
    public float ascendSpeed = 5.0f; 

    [Header("시점 제어 설정")]
    public float mouseSensitivity = 100.0f; // 마우스 감도
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        // 처음 시작할 때 마우스 커서를 보이게 설정
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // ===================================
        // 1. WASD 및 상승/하강 이동 코드 (이전과 동일)
        // ===================================
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = 
            transform.forward * verticalInput * movementSpeed * Time.deltaTime +
            transform.right * horizontalInput * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) 
        {
            movement += transform.up * ascendSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            movement -= transform.up * ascendSpeed * Time.deltaTime;
        }
        
        transform.Translate(movement, Space.World); 


        // ===================================
        // 2. 마우스 시점 회전 (추가됨)
        // WASD 이동과 자연스럽게 연결하려면 이 코드가 필요합니다.
        // 마우스 커서가 잠겨있을 때만 작동합니다.
        // ===================================
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotationX -= mouseY;
            // 상하 시야각을 -90도에서 90도로 제한하여 고개를 꺾는 현상을 방지
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            rotationY += mouseX;

            // 회전 적용: 상하는 로컬 회전, 좌우는 월드 회전 (카메라의 부모 오브젝트가 있다면 부모도 회전시켜야 할 수 있음)
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        // ===================================
        // 3. 커서 제어 기능 (요청하신 기능)
        // ===================================

        // F 키를 누르면 커서 잠금 및 숨기기 (1인칭 시점 모드)
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 커서를 화면 중앙에 고정하고 숨깁니다.
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
            Debug.Log("마우스 커서 잠금 및 숨김 (F)");
        }
        
        // G 키를 누르면 커서 잠금 해제 및 보이기 (UI 상호작용 모드)
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 커서 잠금을 해제하고 보이게 합니다.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("마우스 커서 잠금 해제 및 보임 (G)");
        }
    }
}
