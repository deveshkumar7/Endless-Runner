using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgsound : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}