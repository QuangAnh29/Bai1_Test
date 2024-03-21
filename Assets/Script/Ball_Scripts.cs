using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Scripts : MonoBehaviour
{
    public Material ballColor;

    private void Start()
    {
        GetComponent<Renderer>().material = ballColor;
    }
}
