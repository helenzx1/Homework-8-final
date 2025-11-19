using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;          // 走路速度
    public float gravity = -9.81f;        // 重力

    [Header("视角参数")]
    public float mouseSensitivity = 100f; // 滑鼠灵敏度
    public Transform cameraTransform;     // 头上的 Camera

    private CharacterController controller;
    private float verticalVelocity;       // 垂直速度（重力用）
    private float xRotation = 0f;         // 上下视角角度

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 锁定游标在中央（玩游戏时常见）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        // 取得滑鼠移动量
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 上下看（只转相机）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // 限制不要转到脖子断掉
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // 左右转（转整个角色）
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        // 取得键盘输入（A D 左右、W S 前后）
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 以角色目前朝向为基准移动
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        move = move.normalized * moveSpeed;

        // 重力
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f; // 贴住地面
        }
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }
}
