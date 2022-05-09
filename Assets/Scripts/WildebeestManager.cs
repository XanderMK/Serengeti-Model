using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildebeestManager : MonoBehaviour
{

    public int initialWildebeest;
    [SerializeField] private GameObject wildebeestPrefab;
    public Material normalWildebeest, diseasedWildebeest;

    // Start is called before the first frame update
    void Start()
    {
        initialWildebeest = PlayerPrefs.GetInt("initialWildebeest", 10);
        for (int i = 0; i < initialWildebeest; i++)
        {
            Instantiate(wildebeestPrefab, new Vector3(Random.Range(-49f, 49f), 0.5f, Random.Range(-24f, 24f)), Quaternion.identity).transform.SetParent(transform);
        }
    }
}

