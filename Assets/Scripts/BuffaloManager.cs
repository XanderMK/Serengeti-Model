using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloManager : MonoBehaviour
{

    public int initialBuffalo;
    [SerializeField] private GameObject buffaloPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialBuffalo; i++) {
            Instantiate(buffaloPrefab, new Vector3(Random.Range(-49f, 49f), 0.5f, Random.Range(-24f, 24f)), Quaternion.identity).transform.SetParent(transform);
        }
    }
}
