using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private readonly float growthSpeed = 1f;
    private readonly float rainGrowthMultiplier = 2f; 
    float growth = 35f;
    public bool isBeingRainedOn = false;
    public bool isOnFire = false;

    Transform player;
    SphereCollider col;

    void Awake() {
        player = GameObject.Find("Main Camera").transform;
        col = gameObject.GetComponent<SphereCollider>();
    }

    void FixedUpdate() {
        growth += growthSpeed * Time.fixedDeltaTime;

        growth = Mathf.Clamp(growth, 0f, 100f);

        transform.localPosition = new Vector3(transform.localPosition.x, (growth/100f)-0.5f, transform.localPosition.z);

        col.center = Vector3.up * (((100f-growth)/100)+0.5f);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public int GiveFood(int amount) {
        int amountToReturn = 0;
        if (amount >= growth) {
            amountToReturn = Mathf.FloorToInt(growth);
            growth = 0f;
        } else {
            amountToReturn = amount;
            growth -= amount;
        }

        return amountToReturn;
    }
}