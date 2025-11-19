using UnityEngine;

public class SimpleHover : MonoBehaviour
{
    [Header("上下浮动参数")]
    public float amplitude = 0.2f;   // 浮动幅度（上下移动多少）
    public float frequency = 1f;     // 浮动速度（频率）

    [Header("是否用本地座标移动")]
    public bool useLocalPosition = true;

    [Header("可选：旋转效果")]
    public bool enableRotate = true;
    public Vector3 rotateSpeed = new Vector3(0f, 30f, 0f); // 每秒旋转角度

    private Vector3 startPos;

    void Start()
    {
        // 记录初始位置
        startPos = useLocalPosition ? transform.localPosition : transform.position;
    }

    void Update()
    {
        // 用 Sin 做上下浮动
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        if (useLocalPosition)
        {
            Vector3 pos = transform.localPosition;
            pos.y = startPos.y + offset;
            transform.localPosition = pos;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y = startPos.y + offset;
            transform.position = pos;
        }

        // 旋转（可关）
        if (enableRotate)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
