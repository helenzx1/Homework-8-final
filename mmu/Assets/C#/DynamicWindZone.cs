using UnityEngine;

[RequireComponent(typeof(WindZone))]
public class DynamicWindZoneSafe : MonoBehaviour
{
    [Header("风强度范围（建议先很小再加）")]
    public float mainMin = 0.05f;
    public float mainMax = 0.15f;

    [Header("乱流范围")]
    public float turbulenceMin = 0.05f;
    public float turbulenceMax = 0.20f;

    [Header("变化速度")]
    public float mainLerpSpeed = 0.25f;
    public float turbulenceLerpSpeed = 0.35f;

    [Header("方向轻微摆动")]
    public bool swayDirection = false;          // 默认关，先确认数值稳定
    public float directionAmplitude = 5f;       // 度
    public float directionSpeed = 0.08f;

    // 内部状态
    private WindZone wz;
    private float targetMain, targetTurb;
    private float smoothedMain, smoothedTurb;
    private float timeOffset;

    // 绝对上限
    public float hardMainMax = 0.6f;
    public float hardTurbMax = 0.6f;

    void Start()
    {
        wz = GetComponent<WindZone>();
        wz.mode = WindZoneMode.Directional;

        timeOffset = Random.value * 10f;
        PickNewTargets();

        // 初始化平滑值
        smoothedMain = Mathf.Clamp(targetMain, 0f, hardMainMax);
        smoothedTurb = Mathf.Clamp(targetTurb, 0f, hardTurbMax);

      
    }

    void Update()
    {
       
        smoothedMain = Mathf.Lerp(smoothedMain, targetMain, Time.deltaTime * mainLerpSpeed);
        smoothedTurb = Mathf.Lerp(smoothedTurb, targetTurb, Time.deltaTime * turbulenceLerpSpeed);

        float noise = Mathf.Sin(Time.time * 0.3f + timeOffset) * 0.5f + 0.5f; // 0..1
        float modMain = Mathf.Lerp(0.9f, 1.1f, noise);     // ±10%
        float modTurb = Mathf.Lerp(0.95f, 1.05f, noise);   // ±5%

        float finalMain = Mathf.Clamp(smoothedMain * modMain, 0f, hardMainMax);
        float finalTurb = Mathf.Clamp(smoothedTurb * modTurb, 0f, hardTurbMax);

      
        wz.windMain = finalMain;
        wz.windTurbulence = finalTurb;

     
        if (Time.frameCount % 480 == 0) PickNewTargets();

        //可选：方向轻微摆动
        if (swayDirection)
        {
            float sway = Mathf.Sin(Time.time * directionSpeed) * directionAmplitude;
            transform.rotation = Quaternion.Euler(0f, sway, 0f);
        }
    }

    void PickNewTargets()
    {
        targetMain = Random.Range(mainMin, mainMax);
        targetTurb = Random.Range(turbulenceMin, turbulenceMax);
    }
}
