using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private readonly float growthSpeed = 1.25f;
    private readonly float rainGrowthMultiplier = 2f; 
    public float growth = 60f;
    public bool isBeingRainedOn = false;
    public bool isOnFire = false;

    public SphereCollider col;
    public float growthBeforeAte = 60f;

    void Awake() {
        growth = 60f;
        growthBeforeAte = 60f;

        col = gameObject.GetComponent<SphereCollider>();

        StartCoroutine(Grow());
    }

    IEnumerator Grow() {
        while (true) {
            // LOTS OF FIRE LOGIC
            if (Random.Range(0, 1000000) == 0)
            {
                isOnFire = true;
            }

            if (isOnFire)
            {
                gameObject.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<GrassManager>().grassOnFire;
                if (Random.Range(0, 40) == 0)
                {
                    int borderingGrass = Random.Range(0, 4);
                    if (borderingGrass == 0 && transform.localPosition.x > 0) // Grass to the left of this one gets set on fire
                    {
                        transform.parent.GetComponent<GrassManager>().grass[(int)transform.localPosition.x-1, (int)transform.localPosition.z].GetComponent<Grass>().isOnFire = true;
                    } else if (borderingGrass == 1 && transform.localPosition.z < 49) // Grass to the north of this one gets set on fire
                    {
                        transform.parent.GetComponent<GrassManager>().grass[(int)transform.localPosition.x, (int)transform.localPosition.z+1].GetComponent<Grass>().isOnFire = true;
                    } else if (borderingGrass == 2 && transform.localPosition.x < 99) // Grass to the right of this one gets set on fire
                    {
                        transform.parent.GetComponent<GrassManager>().grass[(int)transform.localPosition.x+1, (int)transform.localPosition.z].GetComponent<Grass>().isOnFire = true;
                    } else if (borderingGrass == 3 && transform.localPosition.z > 0) // Grass to the south of this one gets set on fire
                    {
                        transform.parent.GetComponent<GrassManager>().grass[(int)transform.localPosition.x, (int)transform.localPosition.z-1].GetComponent<Grass>().isOnFire = true;
                    }
                }
                growth -= 4f * 0.2f;
                if (growth <= 0f)
                {
                    isOnFire = false;
                    gameObject.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<GrassManager>().normalGrass;
                }
            }
            // END FIRE LOGIC

            else if (isBeingRainedOn)
                growth += growthSpeed * 0.1f * rainGrowthMultiplier;
            else
                growth += growthSpeed * 0.1f;

            growth = Mathf.Clamp(growth, 0f, 100f);

            yield return new WaitForSeconds(0.2f);
        }
    }

    public float GiveFood(int amount) {
        int amountToReturn = 0;
        growthBeforeAte = growth;
        if (amount >= growth) {
            amountToReturn = Mathf.FloorToInt(growth);
            growth = 0f;
        } else {
            amountToReturn = amount;
            growth -= amount;
        }
        return amountToReturn/10f;
    }
}