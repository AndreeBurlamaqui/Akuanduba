using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetFloat("checkpointPosX", transform.position.x);
        PlayerPrefs.SetFloat("checkpointPosY", transform.position.y);
    }
}
