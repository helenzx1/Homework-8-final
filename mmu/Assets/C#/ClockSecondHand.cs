using UnityEngine;

public class ClockSecondHand : MonoBehaviour
{
    [Header("✔ 勾选 = 顺时针 / 取消 = 逆时针")]
    public bool clockwise = true;

    void Update()
    {
        int seconds = System.DateTime.Now.Second;
        float angle = seconds * 6f;

        // 依照开关决定方向
        float finalAngle = clockwise ? -angle : angle;

        transform.localRotation = Quaternion.Euler(0, finalAngle, 0);
    }
}
