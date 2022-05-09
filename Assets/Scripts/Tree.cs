using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Grass sharedGrass;
    float timeSinceGrassOnFire = 0f;
    float timeUntilBurn = 5f;

    void FixedUpdate()
    {
        if (sharedGrass.isOnFire)
        {
            if (timeSinceGrassOnFire >= timeUntilBurn)
            {
                transform.parent.GetComponent<TreeManager>().trees[(int)transform.position.x + 50, (int)transform.position.z + 25] = null;
                Destroy(gameObject);
            }
            timeSinceGrassOnFire += Time.fixedDeltaTime;
        }
        else
            timeSinceGrassOnFire = 0f;
    }
}
