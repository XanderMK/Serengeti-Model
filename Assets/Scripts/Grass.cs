using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private float growthSpeed;
    [SerializeField] private float rainGrowthMultiplier; 
    public float growth = 0f;
    public bool isBeingRainedOn = false;
    public bool isOnFire = false;

    Transform player;

    void Awake() {
        player = GameObject.Find("Main Camera").transform;
    }

    void FixedUpdate() {
        growth += growthSpeed * Time.fixedDeltaTime;
        if (growth > 100) {
            growth = 100;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, (growth/100f)-0.5f, transform.localPosition.z);

        transform.LookAt(player);
    }

    public int GiveFood(int amount) {
        int amountToReturn = 0;
        if (amount >= growth) {
            amountToReturn = (int)growth;
            growth = 0f;
        } else {
            amountToReturn = amount;
            growth -= amount;
        }

        return amountToReturn;
    }
}
