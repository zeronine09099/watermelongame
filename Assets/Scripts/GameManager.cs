using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isStart = false;
    public int unitCount = 0;
 


    public void MergeManager(int colLoc)
    {
        
    }

    public void UnitDieManager(int unitLevel, GameObject enemy)
    {
        enemy.GetComponent<EnemyScript>().LevelUp(unitLevel);
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

    public void Update()
    {
        if(unitCount == 2)
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
