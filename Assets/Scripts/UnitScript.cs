using UnityEngine;
using UnityEngine.InputSystem;

public class UnitScript : MonoBehaviour
{

    public int level = 1;
    public int id;

    public int basehealth = 1;
    public int baseattack = 1;

    [Header("Control")]
    public float moveSpeed = 5f;     // A/D 이동 속도 (클릭 전)

    [Header("State Flags")]
    public bool isDropped = false;   // 클릭 후 낙하 시작

    private Rigidbody2D rb;
    private Collider2D col;
    private int currentHealth;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        //id = IdProvider.Next(); // 고유 ID 발급
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = basehealth;

        rb.gravityScale = 0; // 초기 중력 비활성화
        rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDropped)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                Debug.Log("aKey pressed");
                Vector2 velocity = rb.velocity;
                velocity.x -= moveSpeed;
                rb.velocity = velocity;
            }
            if (Keyboard.current.dKey.isPressed)
            {
                Debug.Log("dKey pressed");
                Vector2 velocity = rb.velocity;
                velocity.x += moveSpeed;
                rb.velocity = velocity;
            }

            

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                StartDrop();
            }
        }
    }


    private void StartDrop()
    {
        isDropped = true;
        rb.gravityScale = 200; // 중력 활성화
    }
}
