using UnityEngine;

public class GazeInteractor : MonoBehaviour
{
    [Header("Gaze Settings")]
    public float maxDistance = 5f;
    public LayerMask uiLayer;
    public float dwellTime = 1.5f; // 注视触发所需的秒数

    [Header("References")]
    public VRInterfaceController ui;

    private float gazeTimer = 0f;
    private GameObject currentGazeObject;

    private void Start()
    {
        //  Unity 6 推荐的新 API 替换 FindObjectOfType
        if (ui == null)
        {
            ui = Object.FindFirstObjectByType<VRInterfaceController>();
        }

        if (ui == null)
        {
            Debug.LogWarning("GazeInteractor: 未能在场景中找到 VRInterfaceController。");
        }
    }

    void Update()
    {
        // 在 Scene 窗口画出射线以便调试位置是否精准
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // 执行射线检测
        if (Physics.Raycast(ray, out hit, maxDistance, uiLayer))
        {
            // 如果注视的对象发生了变化
            if (currentGazeObject != hit.collider.gameObject)
            {
                ResetGaze(hit.collider.gameObject);
            }
            else
            {
                // 持续注视同一个对象
                gazeTimer += Time.deltaTime;

                // 检查是否达到触发时间
                if (gazeTimer >= dwellTime)
                {
                    ExecuteGazeTrigger();
                }
            }
        }
        else
        {
            // 视线移开了 UI 区域
            if (currentGazeObject != null)
            {
                ResetGaze(null);
            }
        }
    }

    private void ResetGaze(GameObject newObject)
    {
        currentGazeObject = newObject;
        gazeTimer = 0f;

    }

    private void ExecuteGazeTrigger()
    {
        Debug.Log($"[科研数据] Gaze 激活对象: {currentGazeObject.name} 于 {Time.time}s");

        if (ui != null)
        {
            ui.TriggerByGaze();
        }

        // 触发后重置计时，防止在同一帧或下一帧立即重复触发
        gazeTimer = 0f;
    }

    // 获取当前注视完成度的百分比 (0-1)，可用于 UI 进度条显示
    public float GetGazeProgress()
    {
        if (currentGazeObject == null) return 0f;
        return Mathf.Clamp01(gazeTimer / dwellTime);
    }
}

