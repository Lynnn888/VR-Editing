using UnityEngine;
using TMPro;
using System; // 用于记录交互时间

public class VRInterfaceController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text targetText;              // 显示目标句子
    public TMP_InputField errorInputField;   // 用于纠错的输入框

    [Header("Feedback Settings")]
    public Color highlightColor = Color.yellow;
    private Color originalColor = Color.white;

    // 更新目标文本
    public void UpdateTargetText(string newText)
    {
        if (targetText != null)
            targetText.text = newText;
    }

    // 获取当前输入框内容
    public string GetUserInput()
    {
        return errorInputField != null ? errorInputField.text : "";
    }

    // 将模型或处理后的结果填回输入框
    public void ApplyModelOutput(string correctedText)
    {
        if (errorInputField != null)
        {
            errorInputField.text = correctedText;
        }
    }

    // 交互触发方法 

    // Gaze 方法
    public void TriggerByGaze()
    {
        Debug.Log($"[Gaze Event] 触发于: {DateTime.Now:HH:mm:ss.fff}");

        // 自动激活输入框以便后续操作
        if (errorInputField != null)
        {
            errorInputField.ActivateInputField();
        }
    }

    // Hand 方法
    public void TriggerByHand()
    {
        Debug.Log($"[Hand Event] 手势识别成功触发");

        if (errorInputField != null)
        {
            errorInputField.Select();
        }
    }

    // Voice 方法
    public void TriggerByVoice(string voiceCommand)
    {
        Debug.Log($"[Voice Event] 收到指令: {voiceCommand}");

        //如果指令包含特定的修正词，可以直接更新文本
        if (!string.IsNullOrEmpty(voiceCommand))
        {
            
        }
    }
}
