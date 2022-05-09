using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    [SerializeField] private Vector3 velocity;

    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.position += velocity * Time.fixedDeltaTime;

        if (rb.position.x <= -50f || rb.position.x >= 50f)
        {
            velocity.x = -velocity.x;
        }
        if (rb.position.z <= -25f || rb.position.z >= 25f)
        {
            velocity.z = -velocity.z;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Grass"))
        {
            other.gameObject.GetComponent<Grass>().isBeingRainedOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Grass"))
        {
            other.gameObject.GetComponent<Grass>().isBeingRainedOn = false;
        }
    }
}
