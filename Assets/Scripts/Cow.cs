using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = Camera.main.transform;

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
}
