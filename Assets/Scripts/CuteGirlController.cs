using UnityEngine;

/// <summary>
/// 可爱卡通女孩角色控制器
/// 处理角色的基本移动、动画和交互
/// </summary>
public class CuteGirlController : MonoBehaviour
{
    [Header("移动设置")]
    [Tooltip("移动速度")]
    public float moveSpeed = 5f;
    
    [Tooltip("跳跃力度")]
    public float jumpForce = 7f;
    
    [Header("引用设置")]
    [Tooltip("角色SpriteRenderer")]
    public SpriteRenderer characterSprite;
    
    [Tooltip("角色Animator")]
    public Animator animator;
    
    [Tooltip("地面检测点")]
    public Transform groundCheck;
    
    [Tooltip("地面层级")]
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private bool isFacingRight = true;
    
    // 动画参数哈希值
    private int isWalkingHash;
    private int isJumpingHash;
    private int velocityYHash;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 缓存动画参数
        isWalkingHash = Animator.StringToHash("IsWalking");
        isJumpingHash = Animator.StringToHash("IsJumping");
        velocityYHash = Animator.StringToHash("VelocityY");
        
        Debug.Log("💕 可爱的卡通女孩登场啦！");
    }
    
    void Update()
    {
        // 地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // 水平移动输入
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // 移动
        Move(horizontalInput);
        
        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
        // 更新动画
        UpdateAnimation(horizontalInput);
    }
    
    /// <summary>
    /// 角色移动
    /// </summary>
    void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        
        // 翻转角色朝向
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    /// <summary>
    /// 角色跳跃
    /// </summary>
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("✨ 跳起来啦！");
    }
    
    /// <summary>
    /// 翻转角色朝向
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    /// <summary>
    /// 更新动画状态
    /// </summary>
    void UpdateAnimation(float horizontalInput)
    {
        if (animator == null) return;
        
        // 行走动画
        bool isWalking = Mathf.Abs(horizontalInput) > 0.01f;
        animator.SetBool(isWalkingHash, isWalking);
        
        // 跳跃动画
        animator.SetBool(isJumpingHash, !isGrounded);
        animator.SetFloat(velocityYHash, rb.velocity.y);
    }
    
    /// <summary>
    /// 触发互动效果
    /// </summary>
    public void PlayCuteEffect()
    {
        Debug.Log("💖 爱心特效触发！");
        // 这里可以添加粒子效果等
    }
    
    void OnDrawGizmosSelected()
    {
        // 绘制地面检测范围
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
