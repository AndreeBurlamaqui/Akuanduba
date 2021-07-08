using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManagement : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Assets = new List<GameObject>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            Assets.Add(child.gameObject);
        }

        foreach(GameObject obj in Assets)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in Assets)
            {
                obj.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in Assets)
            {
                obj.SetActive(false);
            }
        }
    }
}
