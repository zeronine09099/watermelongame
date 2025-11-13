using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameManager gameManager;

    public int baseAttack = 1;
    public int baseHealth = 1;
    public int level = 1;
    public int speed = 400;
    enum EnemyMoveType { Left, Right };

    public int attack = 0;
    public int health = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        attack = baseAttack;
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            gameManager.CombatManager(gameObject, collision.gameObject);
        }
    }

    public void DecreaseHealth(int unitAttack)
    {
        health -= unitAttack;
        if (health <= 0)
        {
            Die();
        }   
    }

    private void Die()
    {
        //Todo : 점수 증가
        //Todo : 필드 내 적 제거 함수 호출
        gameManager.EnemyDieManager(level);
        Destroy(gameObject);
    }

    public void LevelUp(int unitLevel)
    {
        if (level == unitLevel)
        {
            health += (int)(Mathf.Pow(2, level - 1));
            attack += (int)(Mathf.Pow(2, level - 1));
            level += 1;
        }
    }
}
