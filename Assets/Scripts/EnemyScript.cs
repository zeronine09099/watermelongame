using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameManager gameManager;

    public int baseAttack = 1;
    public int baseHealth = 1;
    public int level = 1;
    enum EnemyMoveType { Left, Right };

    public int attack = 0;
    public int health = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack = baseAttack;
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            gameManager.CombatManager(object, collision.gameObject);
        }
    }

    private void DecreaseHealth(int unitAttack)
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
        Destroy(gameObject);
    }

    private void LevelUp(int unitLevel)
    {
        if (level == unitLevel)
        {
            health += Math.Pow(2, level - 1);
            attack += Math.Pow(2, level - 1);
            level += 1;
        }
    }
}
