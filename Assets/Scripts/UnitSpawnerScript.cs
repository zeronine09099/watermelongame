using UnityEngine;

public class UnitSpawnerScript : MonoBehaviour
{
    public GameObject[] previewObjects;
    public GameObject[] objects;
    public bool isReady = true;
    public int rand = 1;
    public int idNum = 1;

    public void Ready()
    {
        Debug.Log("ready!");
        SpawnUnit();
        SpawnNextUnit();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNextUnit();
        Ready();
    }



    // Update is called once per frame

    void SpawnUnit()
    { 
        var go = Instantiate(objects[rand -1], transform.position, Quaternion.identity);
        var unit = go.GetComponent<UnitScript>();
        unit.id = idNum;
            
       
        //유닛 스폰
    }

    public void SpawnMergeUnit(int level,Vector2 colLoc)
    {
        Debug.Log("Merge Spawn");
        idNum++;
        level -= 1;
        var go = Instantiate(objects[level], colLoc, Quaternion.identity);
        var unit = go.GetComponent<UnitScript>();
        unit.id = idNum;
        unit.Merged();
        //머지 유닛 스폰
    }

    void SpawnNextUnit()
    {
        foreach (GameObject obj in previewObjects)
        {
            obj.SetActive(false);
        }
        idNum++;
        rand = Random.Range(1, 6);
        previewObjects[rand-1].SetActive(true);

        //다음 유닛 스폰
    }

}
