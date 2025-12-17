using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public VRInterfaceController ui;   // UI 控制器引用

    void Start()
    {
        // 如果 Inspector 里没拖，就自动查找
        if (ui == null)
            ui = FindObjectOfType<VRInterfaceController>();

        if (ui == null)
        {
            Debug.LogError("❌ VRInterfaceController not found in the scene!");
            return;
        }

        // 初始化一个句子展示
        ui.UpdateTargetText("Please correct this sentence:");

        // 也可以初始化 error 区域
        ui.ApplyModelOutput("This is an errr sentence.");
    }

    void Update()
    {
        // 按 Enter 模拟一次“提交”
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string userText = ui.GetUserInput();
            Debug.Log("User Input: " + userText);

            // 假装这是 LLM 修正后的结果
            string corrected = userText + " [corrected by model]";
            ui.ApplyModelOutput(corrected);
        }
    }

    // ========= 外部可调用的触发函数 =========

    public void TriggerGaze()
    {
        if (ui != null)
            ui.TriggerByGaze();
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


