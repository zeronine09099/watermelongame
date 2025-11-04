using UnityEngine;

public class UnitSpawnerScript : MonoBehaviour
{
    public GameObject[] previewObjects;
    public GameObject[] objects;
    public bool isReady = true;
    public int rand;

    public void Ready()
    {
        isReady = true;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNextUnit();
        SpawnUnit();
        SpawnNextUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if isReady
        {
            SpawnUnit();
            SpawnNextUnit();
            isReady = false;
        }
    }

    void SpawnUnit()
    {
        switch(rand)
        {
            case 1:
                Instantiate(objects[1], transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(objects[2], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(objects[3], transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(objects[4], transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(objects[5], transform.position, Quaternion.identity);
                break;
        }
        //유닛 스폰
    }

    void SpawnNextUnit()
    {
        foreach (GameObject obj in previewObjects)
        {
            obj.SetActive(false);
        }
        rand = Random.Range(1, 6);
        objects[rand].SetActive(true);

        //다음 유닛 스폰
    }

}
