using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    //Error
                    Debug.LogError("There is no GameManager in the scene.");
                }
            }
            return _instance;
        }
    }
    private static GameManager _instance;

    public bool isStart = false;
    public int unitCount = 0;
    public int enemyCount = 0;
    public UnitSpawnerScript unitSpawner;
    public EnemySpawnerScript enemySpawnerScript;

    private void Start()
    {
        Debug.Log(unitSpawner);
        Debug.Log(gameObject.GetInstanceID());
    }
 
    public void MergeManager(Vector2 colLoc,GameObject unit1, GameObject unit2,int level)
    {
        UnitScript unitScript1 = unit1.GetComponent<UnitScript>();
        UnitScript unitScript2 = unit2.GetComponent<UnitScript>();
        unitScript1.Delete(unit1);
        unitScript2.Delete(unit2);
        Debug.Log(gameObject.GetInstanceID());
        Debug.Log(unitSpawner);
        unitSpawner.SpawnMergeUnit(level+1, colLoc);
        
    }

    public void UnitDieManager(int unitLevel, GameObject enemy)
    {
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        enemyScript.LevelUp(unitLevel);
    }

    public void CombatManager (GameObject enemy, GameObject unit)
    {       
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        UnitScript unitScript = unit.GetComponent<UnitScript>();
        //적이 유닛 공격
        unitScript.DecreaseHealth(enemyScript.attack,enemy);
        //유닛이 적 공격
        enemyScript.DecreaseHealth(unitScript.baseattack);
    }

    public void UnitPlusManager()
    {
        unitCount++;
    }

    public void UnitMinusManager()
    {
        unitCount--;
    }

    public void EnemyCountdown()
    {
        enemySpawnerScript.currentCooltime -= 1;
    }

    public void EnemyDieManager(int level)
    {
        enemyCount--;
        if (enemyCount == 0 && enemySpawnerScript.isSpawning == false)
        {
            UnitScript.stopSpawn = false;
            unitSpawner.Ready();
        }
    }

    public void StopSpawnManager()
    {
        UnitScript.stopSpawn = true;
    }

    public void EnemyPlusManager()
    {
        enemyCount++;
    }   
    public void Update()
    {
        if (unitCount == 2)
        {
            if (isStart == false)
            {
                isStart = true;
                unitCount -= 1;
            }
        }

        if(unitCount == 0)
        {
            GameOverManager();
        }


    }

    public void GameOverManager()
    {
        Debug.Log("Game Over");
    }

}
