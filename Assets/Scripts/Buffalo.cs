using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : MonoBehaviour
{
    private readonly float moveSpeed = 1f;
    private readonly float ageToReproduce = 60f;
    private readonly float energyToReproduce = 80f;
    private readonly float minEnergyToStayAlive = 10f;
    private readonly float energyDepletionRate = 3f;

    float timeAlive;
    float energy = 50f;

    Vector2 moveDirection = Vector2.zero;

    Transform player;

    private void Awake()
    {

        timeAlive = 0f;
        energy = 50f;

        while (moveDirection == Vector2.zero)
            moveDirection = Random.insideUnitCircle;

        player = GameObject.Find("Main Camera").transform;
    }

    private void FixedUpdate()
    {

        energy -= energyDepletionRate * Time.fixedDeltaTime;
        if (energy < minEnergyToStayAlive)
            Destroy(gameObject);
        timeAlive += Time.fixedDeltaTime;

        if (energy >= energyToReproduce && timeAlive >= ageToReproduce)
        {
            Instantiate(gameObject, transform.position, Quaternion.identity);
            energy -= 60f;
            timeAlive = ageToReproduce * (2f / 3f);
        }

        transform.position += new Vector3(moveDirection.x, 0f, moveDirection.y) * moveSpeed * Time.fixedDeltaTime;

        if (transform.position.x <= -50f || transform.position.x >= 50f)
            moveDirection.x = -moveDirection.x;
        if (transform.position.z <= -25f || transform.position.z >= 25f)
            moveDirection.y = -moveDirection.y;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Grass") && energy <= 100f)
        {
            energy += other.gameObject.GetComponent<Grass>().GiveFood(20);
        }
    }
}
