using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    [SerializeField] private int arrayWidth;
    [SerializeField] private int arrayHeight;

    public GameObject grassPrefab;

    Transform[,] grass;

    // Start is called before the first frame update
    void Start()
    {
        grass = new Transform[arrayWidth, arrayHeight];

        for (int i = 0; i < arrayWidth; i++) {
            for (int j = 0; j < arrayHeight; j++) {
                grass[i, j] = Instantiate(grassPrefab).transform;
                grass[i, j].localPosition = new Vector3(i, -0.5f, j);
                grass[i, j].gameObject.name = "Grass: " + i + ", " + j;
                grass[i, j].SetParent(transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
