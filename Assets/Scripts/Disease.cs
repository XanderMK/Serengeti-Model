using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour
{
    public Vector2 velocity;
    private Transform player;

    private void Start()
    {
        player = Camera.main.transform;
        StartCoroutine(Kill());
        StartCoroutine(VisualUpdate());
    }
    
    void FixedUpdate()
    {
        transform.position += new Vector3(velocity.x, 0f, velocity.y) * Time.fixedDeltaTime;
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
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
}
