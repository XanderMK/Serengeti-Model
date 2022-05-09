using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildebeest : MonoBehaviour
{
    private readonly float moveSpeed = 1.75f;
    private readonly float ageToReproduce = 60f;
    private readonly float energyToReproduce = 100f;
    private readonly float minEnergyToStayAlive = 10f;
    private readonly float energyDepletionRate = 5f;

    float timeAlive;
    float energy = 50f;

    public bool isSick = false;
    float timeSinceSick = 0f;
    float timeUntilDieFromSickness;

    Vector3 moveDirection = Vector2.zero;

    Transform player;
    Transform rain;
    Rigidbody rb;

    private void Awake()
    {

        timeAlive = 0f;
        energy = 50f;
        timeUntilDieFromSickness = Random.Range(15f, 45f);

        while (moveDirection == Vector3.zero)
        {
            Vector3 randDir = Random.insideUnitSphere;
            moveDirection = new Vector3(randDir.x, 0f, randDir.z).normalized;
        }


        player = Camera.main.transform;
        rain = GameObject.Find("Rain").transform;
        rb = gameObject.GetComponent<Rigidbody>();

        StartCoroutine(ExtraLogic());
        StartCoroutine(VisualUpdate());
    }

    private IEnumerator VisualUpdate()
    {
        while (true)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.Rotate(Vector3.up * 180f);

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    private IEnumerator ExtraLogic()
    {
        while (true)
        {
            energy -= energyDepletionRate * 0.1f;
            if (energy < minEnergyToStayAlive)
                Destroy(gameObject);
            if (isSick) // Sickness checks
            {
                timeSinceSick += 0.1f;
                if (timeSinceSick >= timeUntilDieFromSickness)
                {
                    if (!(Random.Range(0, 40) == 0))
                    {
                        Destroy(gameObject);
                    } else
                    {
                        isSick = false;
                        gameObject.GetComponent<MeshRenderer>().material = GetComponentInParent<WildebeestManager>().normalWildebeest;
                    }
                }
                    
            }
            timeAlive += 0.1f;

            if (energy >= energyToReproduce && timeAlive >= ageToReproduce && !isSick)
            {
                Instantiate(gameObject, transform.position, Quaternion.identity).transform.SetParent(transform.parent);
                energy -= 60f;
                timeAlive = ageToReproduce * 0.75f;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }

    private void FixedUpdate()
    {
        rb.position += moveDirection * moveSpeed * Time.fixedDeltaTime;

        if (rb.position.x <= -50f || rb.position.x >= 50f)
            moveDirection.x = -moveDirection.x;
        if (rb.position.z <= -25f || rb.position.z >= 25f)
            moveDirection.z = -moveDirection.z;

        if (rain.position.x - 6f > transform.position.x)
            moveDirection.x = Mathf.Lerp(moveDirection.x, 1f, 0.005f);
        else if (rain.position.x + 6f < transform.position.x)
            moveDirection.x = Mathf.Lerp(moveDirection.x, -1f, 0.005f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Grass") && energy <= 100f)
        {
            energy += other.gameObject.GetComponent<Grass>().GiveFood(15);
        } else if (other.gameObject.name.Contains("Disease") || (other.gameObject.name.Contains("Wildebeest") && other.gameObject.GetComponent<Wildebeest>().isSick) || (other.gameObject.name.Contains("Buffalo") && other.gameObject.GetComponent<Buffalo>().isSick))
        {
            isSick = true;
            gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponentInParent<WildebeestManager>().diseasedWildebeest;
        }
    }
}