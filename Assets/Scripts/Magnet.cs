using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject _myPlayer;
    public static bool _magnetActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //For Magnet
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //transform.Rotate(150 * Time.deltaTime, 0, 0);
        if (_magnetActive)
        {
            MoveTowardsPlayer();
        }
        //coinsText.text = "Coins : " + NumberOfCoins;
    }

    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, _myPlayer.transform.position) < 10)
            transform.position = Vector3.MoveTowards(this.transform.position, _myPlayer.transform.position, 40f * Time.deltaTime);
           
    }


}
