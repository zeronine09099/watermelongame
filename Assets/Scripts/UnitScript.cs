using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitScript : MonoBehaviour
{
    private GameManager gameManager;
    public UnitSpawnerScript unitSpawner;

    public int level = 1;
    public int id;
    public bool isHit = false;
    public static bool stopSpawn = false;
    public float scaleMultiplier = 1.5f;

    public int basehealth = 1;
    public int baseattack = 1;

    [Header("Control")]
    public float moveSpeed = 3f;     // A/D 이동 속도 (클릭 전)

    [Header("State Flags")]
    public bool isDropped = false;   // 클릭 후 낙하 시작

    private Rigidbody2D rb;
    private Collider2D col;
    public int currentHealth;


    void Awake()
    {
        Debug.Log("Spawned!");
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = 0; // 초기 중력 비활성화
        //id = IdProvider.Next(); // 고유 ID 발급

        gameManager = GameManager.Instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = basehealth;
        Vector3 scale = transform.localScale;
        scale.x *= scaleMultiplier;
        scale.y *= scaleMultiplier;
        transform.localScale = scale;

        rb.linearVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDropped)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                
                Vector2 velocity = rb.linearVelocity;
                velocity.x -= moveSpeed;
                rb.linearVelocity = velocity;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                
                Vector2 velocity = rb.linearVelocity;
                velocity.x += moveSpeed;
                rb.linearVelocity = velocity;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                StartDrop();
                // Todo: 필드 내 유닛 추가 함수 호출
                gameManager.UnitPlusManager();
                // Todo : 적 소환 카운트다운 함수 호출
                gameManager.EnemyCountdown();
            }


        }
    }

    //Todo : 충돌 시 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.CompareTag("Ground")) // 땅과 충돌 시 처리
        {
            if(isHit == false && isDropped)
            {
                if(stopSpawn == true)
                {
                    isHit = true;
                }

                else
                {
                    Debug.Log("Ground Hit");
                    isHit = true;
                    unitSpawner.Ready();
                }
                    
            } 
        }

        else if (collision.gameObject.CompareTag("Unit")) // 다른 유닛과 충돌 시 처리
        {
            UnitScript otherUnit = collision.gameObject.GetComponent<UnitScript>();
            Vector2 hitPoint = collision.contacts[0].point;

            if (isHit == false && isDropped)
            {
                if (stopSpawn == true)
                {
                    isHit = true;
                }

                else
                {
                    isHit = true;
                    unitSpawner.Ready();
                }
                    
            }

            if (id > otherUnit.id && level == otherUnit.level && isDropped)
            {
                Debug.Log(unitSpawner);
                gameManager.MergeManager(hitPoint, gameObject,collision.gameObject,level);
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
        Delete(gameObject);
    }
        //Todo : 체력 0 시 처리

    public void Delete(GameObject unit)
    {
        Destroy(unit);
    }

    private void StartDrop()
    {
        isDropped = true;
        rb.gravityScale = 500; // 중력 활성화
    }

    public void Merged()
    {
        isHit = true;
        StartDrop();
    }
}
