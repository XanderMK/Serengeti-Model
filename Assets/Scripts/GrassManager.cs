using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public readonly int arrayWidth = 100;
    public readonly int arrayHeight = 50;

    public GameObject grassPrefab;
    public Material normalGrass;
    public Material grassOnFire;

    public Transform[,] grass;
    Transform player;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = Camera.main.transform;

        grass = new Transform[arrayWidth, arrayHeight];

        for (int i = 0; i < arrayWidth; i++) {
            for (int j = 0; j < arrayHeight; j++) {
                Transform grassObj = Instantiate(grassPrefab).transform;
                grassObj.localPosition = new Vector3(i, -0.5f, j);
                grassObj.gameObject.name = "Grass: " + i + ", " + j;
                grassObj.SetParent(transform);
                grass[i, j] = grassObj;
            }
        }

        transform.position = new Vector3(-arrayWidth/2, 0f, -arrayHeight/2);

        StartCoroutine(FakeUpdate());
    }

    void OnDisable() {
        for (int i = 0; i < arrayWidth; i++) {
            for (int j = 0; j < arrayHeight; j++) {
                Destroy(grass[i, j].gameObject);
            }
        }
        grass = null;
    }

    IEnumerator FakeUpdate() {
        while (true) {
            for (int i = 0; i < arrayWidth; i++) {
                for (int j = 0; j < arrayHeight; j++) {
                    Transform grassObj = grass[i, j];
                    grassObj.LookAt(new Vector3(player.position.x, grassObj.position.y, player.position.z));
                    grassObj.Rotate(Vector3.up * 180f);
                    grassObj.localPosition = new Vector3(grassObj.localPosition.x, (grassObj.GetComponent<Grass>().growth/100f)-0.5f, grassObj.localPosition.z);
                    grassObj.GetComponent<SphereCollider>().center = Vector3.up * (((100f-grassObj.GetComponent<Grass>().growth)/100)+0.5f);
                }
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
        
    }
}
