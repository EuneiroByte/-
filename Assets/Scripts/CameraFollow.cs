using UnityEngine;

/// <summary>
/// 相机跟随脚本
/// 让相机平滑跟随可爱的卡通女孩
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("跟随设置")]
    [Tooltip("要跟随的目标")]
    public Transform target;
    
    [Tooltip("平滑度")]
    public float smoothSpeed = 0.125f;
    
    [Tooltip("相机偏移")]
    public Vector3 offset = new Vector3(0, 2, -10);
    
    [Header("边界设置")]
    [Tooltip("是否启用边界限制")]
    public bool useBounds = false;
    
    [Tooltip("相机左边界")]
    public float leftBound = -10f;
    
    [Tooltip("相机右边界")]
    public float rightBound = 10f;
    
    [Tooltip("相机上边界")]
    public float topBound = 5f;
    
    [Tooltip("相机下边界")]
    public float bottomBound = -5f;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // 计算目标位置
        Vector3 desiredPosition = target.position + offset;
        
        // 平滑移动
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // 应用边界限制
        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftBound, rightBound);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, bottomBound, topBound);
        }
        
        transform.position = smoothedPosition;
    }
}
