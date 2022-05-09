using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float initialTrees;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GrassManager grassManager;

    public Transform[,] trees;
    Transform player;

    private void Start()
    {
        initialTrees = PlayerPrefs.GetInt("initialTrees", 100);

        trees = new Transform[grassManager.arrayWidth, grassManager.arrayHeight];
        player = Camera.main.transform;

        for (int i = 0; i < initialTrees; i++)
        {
            Transform tree = Instantiate(treePrefab, new Vector3(Random.Range(-50, 50), 1f, Random.Range(-25, 25)), Quaternion.identity).transform;
            tree.parent = transform;
            tree.GetComponent<Tree>().sharedGrass = grassManager.grass[(int)tree.position.x+50, (int)tree.position.z+25].GetComponent<Grass>();
            trees[(int)tree.position.x + 50, (int)tree.position.z + 25] = tree;
        }

        StartCoroutine(FakeUpdate());
    }

    IEnumerator FakeUpdate()
    {
        while (true)
        {
            for (int i = 0; i < trees.GetLength(0); i++)
            {
                for (int j = 0; j < trees.GetLength(1); j++)
                {
                    Transform treeObj = trees[i, j];
                    if (treeObj != null)
                    {
                        treeObj.LookAt(new Vector3(player.position.x, treeObj.position.y, player.position.z));
                        treeObj.Rotate(Vector3.up * 180f);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }

    }
}
