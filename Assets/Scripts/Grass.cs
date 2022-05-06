using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private readonly float growthSpeed = 1f;
    private readonly float rainGrowthMultiplier = 2f; 
    public float growth = 60;
    public bool isBeingRainedOn = false;
    public bool isOnFire = false;

    public SphereCollider col;

    void Awake() {
        col = gameObject.GetComponent<SphereCollider>();

        StartCoroutine(Grow());
    }

    IEnumerator Grow() {
        while (true) {
            growth += growthSpeed * 0.1f;
            growth = Mathf.Clamp(growth, 0f, 100f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public float GiveFood(int amount) {
        int amountToReturn = 0;
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