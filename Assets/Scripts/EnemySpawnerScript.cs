using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public int cooltime = 20;
    public int spawnCount = 5;
    public int level = 1;
    public bool isSpawning = false;

    public int currentCooltime = 1;

    private GameManager gameManager;
    public GameObject[] enemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        currentCooltime = cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooltime == 0)
        {
            isSpawning = true;
            currentCooltime = cooltime;
            spawn();
            gameManager.StopSpawnManager();
        }

        
    }

    private async Task spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            await Task.Delay(500);
            Instantiate(enemyPrefab[level - 1], transform.position, Quaternion.identity);
            gameManager.EnemyPlusManager();
        }
        isSpawning = false;
        if (level <= 2)level += 1;
    }
}
