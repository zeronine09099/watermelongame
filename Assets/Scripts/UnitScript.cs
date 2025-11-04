using UnityEngine;
using UnityEngine.InputSystem;

public class UnitScript : MonoBehaviour
{
    public GameManager gameManager;
    public UnitSpawnerScript unitSpawner;

    public int level = 1;
    public int id;
    public bool isHit = false;

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
                // Todo: 필드 내 유닛 추가 함수 호출
                gameManager.UnitPlusManager();
                // Todo : 적 소환 카운트다운 함수 호출

            }
        }
    }

    //Todo : 충돌 시 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.CompareTag("Ground")) // 땅과 충돌 시 처리
        {
            if(isHit == false)
            {
                isHit = true;
                unitSpawner.Ready();
            } 
        }

        else if (collision.gameObject.CompareTag("Unit") // 다른 유닛과 충돌 시 처리
        {
            UnitScript otherUnit = collision.gameObject.GetComponent<UnitScript>();
            Vector2 hitPoint = collision.contacts[0].point;

            if (isHit == false)
            {
                isHit = true;
                unitSpawner.Ready();
            }

            if (id > otherUnit.id && level == otherUnit.level)
            {
                gameManager.MergeManager(hitPoint);
            }
            
        }
    }

    public void DecreaseHealth(int damage,GameObject enemy)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(enemy);
        }
    }

    private void Die(GameObject enemy)
    {
        gameManager.UnitMinusManager();
        gameManager.UnitDieManager(level, enemy);
        Destroy (object);
    }
        //Todo : 체력 0 시 처리



    private void StartDrop()
    {
        isDropped = true;
        rb.gravityScale = 200; // 중력 활성화
    }
}
