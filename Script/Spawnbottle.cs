using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Spawnbottle : MonoBehaviour
{
    public static Spawnbottle instance;
    public GameObject bottlePrefab;
    private bool panelWasClosed = true;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.PlayPanle.activeSelf && panelWasClosed)            
        {
            SpawnBottle();
            panelWasClosed = false;
        }
        else if (!UIManager.Instance.PlayPanle.activeSelf)
        {
            panelWasClosed = true;
        }
    }
    public void SpawnBottle()
    {
        if (transform.position != null && bottlePrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Debug.Log("+++" + spawnPosition);
            Instantiate(bottlePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("BottlePrefab or Player is not assigned!");
        }
    }
}
