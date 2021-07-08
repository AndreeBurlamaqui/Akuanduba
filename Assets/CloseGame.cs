using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    void Start()
    {

     StartCoroutine(Close());
    }

    private IEnumerator Close()
    {

        yield return new WaitForSecondsRealtime(6f);

        Application.Quit();
    }
}
