using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;
    private void Awake()
    {
        instance = this;
    }
    /* public GameObject[] objectsSpawn;
    public Transform table;
    public Vector2 spawnAreaSize;
    public LayerMask obstacleLayer;
    //public float tableSurfaceY;



    void Start()
    {
        SpawnObjectsInEmptyCorners();
    }

    public void SpawnObjectsInEmptyCorners()
    {
        Vector3[] corners = new Vector3[]
        {
            new Vector3(0, table.position.y - spawnAreaSize.y, 0),
            new Vector3(table.position.x + spawnAreaSize.x, table.position.y - spawnAreaSize.y, 0)
        };

        ShuffleArray(corners);

        foreach (Vector3 corner in corners)
        {
            if (IsPositionEmpty(corner))
            {
                SpawnObjectAtPosition(corner);
                break;
            }
        }
    }
    private void ShuffleArray(Vector3[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Vector3 temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    private bool IsPositionEmpty(Vector3 position)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(position, obstacleLayer);
        return hitCollider == null;
    }

    public void SpawnObjectAtPosition(Vector3 position)
    {
        GameObject objectToSpawn = objectsSpawn[Random.Range(0, objectsSpawn.Length)];
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }*/


    public GameObject[] obstaclePrefab;
    public Transform[] emptyPositions;
    public LayerMask bottleLayer; 

    private GameObject currentObstacle;

    void Start()
    {
        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        if (emptyPositions.Length > 0)
        {
            int index = Random.Range(0, emptyPositions.Length);
            currentObstacle = Instantiate(obstaclePrefab[Random.Range(0, obstaclePrefab.Length)],
                emptyPositions[index].position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No empty positions available to spawn obstacles.");
        }
    }

    /*public void SpawnTwoObstacles()
    {
        if (emptyPositions.Length >= 1)
        {
            //Destroy(currentObstacle);

            int index1 = Random.Range(0, emptyPositions.Length);
            int index2 = Random.Range(0, emptyPositions.Length);
            while (index1 == index2)
            {
                index2 = Random.Range(0, emptyPositions.Length);
            }

            Instantiate(obstaclePrefab[Random.Range(0, obstaclePrefab.Length)],
                emptyPositions[index1].position, Quaternion.identity);
            Instantiate(obstaclePrefab[Random.Range(0, obstaclePrefab.Length)],
                emptyPositions[index2].position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Not enough empty positions available to spawn obstacles.");
        }

    }*/
}
