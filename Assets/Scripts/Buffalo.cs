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

        StartCoroutine(ExtraLogic());
    }

    private IEnumerator ExtraLogic() {
        while (true) {
            energy -= energyDepletionRate * 0.1f;
            if (energy < minEnergyToStayAlive)
                Destroy(gameObject);
            timeAlive += 0.1f;

            if (energy >= energyToReproduce && timeAlive >= ageToReproduce)
            {
                Instantiate(gameObject, transform.position, Quaternion.identity).transform.SetParent(transform.parent);
                energy -= 60f;
                timeAlive = ageToReproduce * 0.75f;
            }

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.Rotate(Vector3.up * 180f);

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(moveDirection.x, 0f, moveDirection.y) * moveSpeed * Time.fixedDeltaTime;

        if (transform.position.x <= -50f || transform.position.x >= 50f)
            moveDirection.x = -moveDirection.x;
        if (transform.position.z <= -25f || transform.position.z >= 25f)
            moveDirection.y = -moveDirection.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Grass") && energy <= 100f && other.gameObject.GetComponent<Grass>().growth >= 40)
        {
            energy += other.gameObject.GetComponent<Grass>().GiveFood(25);
        }
    }
}
