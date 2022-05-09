using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseManager : MonoBehaviour
{
    public float diseaseSpeed;
    public bool vaccinated;
    [SerializeField] private GameObject diseasePrefab;

    private void Start()
    {
        if (PlayerPrefs.GetInt("vaccinated", 0) == 0)
            vaccinated = false;
        else
            vaccinated = true;
        StartCoroutine(SpawnDisease());
    }

    IEnumerator SpawnDisease()
    {
        while (true)
        {
            if (!vaccinated && Random.Range(0, 100) < diseaseSpeed)
            {
                Instantiate(diseasePrefab, new Vector3(50f, 0.5f, Random.Range(-25, 25)), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.05f);
        }
        
    }
}
