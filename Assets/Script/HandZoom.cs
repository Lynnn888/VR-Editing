using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class HandZoom : MonoBehaviour
{
    [Header("UI 引用")]
    public RectTransform uiPanel; // 确保是 RectTransform 类型

    [Header("缩放设置")]
    public float minScale = 0.5f;
    public float maxScale = 3.0f;

    private float _initialDistance;
    private Vector3 _initialScale;
    private XRHandSubsystem _handSubsystem;

    void Update()
    {
        if (_handSubsystem == null)
        {
            _handSubsystem = XRGeneralSettings.Instance.Manager.activeLoader?.GetLoadedSubsystem<XRHandSubsystem>();
            return;
        }

        var leftHand = _handSubsystem.leftHand;
        var rightHand = _handSubsystem.rightHand;

        // 直接通过关节距离判断捏合，避开 API 版本差异报错
        if (IsPinching(leftHand) && IsPinching(rightHand))
        {
            if (leftHand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose lp) &&
                rightHand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose rp))
            {
                float currentDistance = Vector3.Distance(lp.position, rp.position);

                if (_initialDistance <= 0)
                {
                    _initialDistance = currentDistance;
                    _initialScale = uiPanel.localScale;
                }
                else
                {
                    float factor = currentDistance / _initialDistance;
                    uiPanel.localScale = Vector3.ClampMagnitude(_initialScale * factor, maxScale);
                    // 确保缩放不小于最小值
                    if (uiPanel.localScale.x < minScale) uiPanel.localScale = Vector3.one * minScale;
                }
            }
        }
        else
        {
            _initialDistance = 0f;
        }
    }

    bool IsPinching(XRHand hand)
    {
        if (hand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose index) &&
            hand.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumb))
        {
            return Vector3.Distance(index.position, thumb.position) < 0.02f;
        }
        return false;
    }
}
