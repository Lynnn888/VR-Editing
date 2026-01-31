using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public VRInterfaceController ui;   // UI 控制器引用

    void Start()
    {
        if (ui == null)
            ui = Object.FindFirstObjectByType<VRInterfaceController>();

        if (ui == null)
        {
            Debug.LogError("❌ VRInterfaceController not found in the scene! Please assign it in Inspector.");
            return;
        }

        // 2. 初始化显示
        ui.UpdateTargetText("Please correct this sentence:");
        ui.ApplyModelOutput("This is an errr sentence.");
    }

    void Update()
    {
        // 3. PC 调试模拟：按 Enter 键触发逻辑
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        string userText = ui.GetUserInput();
        if (string.IsNullOrEmpty(userText)) return;

        Debug.Log("User Input Captured: " + userText);

        // 模拟模型处理
        string corrected = userText.Replace("errr", "error") + " [System Processed]";
        ui.ApplyModelOutput(corrected);
    }

    // 外部交互触发接口 

    public void TriggerGaze()
    {
        if (ui != null)
        {
            ui.TriggerByGaze();
        }
    }

    public void TriggerHand()
    {
        if (ui != null)
            ui.TriggerByHand();
    }

    public void TriggerVoice(string voiceCmd)
    {
        if (ui != null)
            ui.TriggerByVoice(voiceCmd);
    }
}


