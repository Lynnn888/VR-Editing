using UnityEngine;
using TMPro;
public class VRInterfaceController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text targetText;              // 显示 target sentence
    public TMP_InputField errorInputField;   // 显示/输入 error sentence

    // target sentence
    public void UpdateTargetText(string newText)
    {
        if (targetText != null)
            targetText.text = newText;
    }

    // 获取用户在 InputField 里输入的句子
    public string GetUserInput()
    {
        if (errorInputField != null)
            return errorInputField.text;

        return "";
    }

    // LLM 修改后结果显示在 InputField 中
    public void ApplyModelOutput(string correctedText)
    {
        if (errorInputField != null)
            errorInputField.text = correctedText;
    }

    //gaze方法
    public void TriggerByGaze()
    {
        Debug.Log("Gaze Triggered!");
    }

    //touch方法
    public void TriggerByHand()
    {
        Debug.Log("Hand Gesture Triggered!");
    }

    //voice方法
    public void TriggerByVoice(string voiceCommand)
    {
        Debug.Log("Voice Received: " + voiceCommand);
    }
}
