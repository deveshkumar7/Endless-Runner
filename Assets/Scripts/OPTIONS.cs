using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OPTIONS : MonoBehaviour
{
    public GameObject control;
    public GameObject credit;

    void Start()
    {
        control.SetActive(true);
        credit.SetActive(false);

    }

    public void Control()
    {
        control.SetActive(true);
        credit.SetActive(false);

    }
    public void Credits()
    {
        control.SetActive(false);
        credit.SetActive(true);

    }


    public void Back()
    {
        Debug.Log("worldloaded");
        SceneManager.LoadScene("Main Menu");

    }
}
