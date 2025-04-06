using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class CoinCollecter : MonoBehaviour
{
    public AudioSource SoundEffects;
    public AudioClip Coins;
    public TextMeshProUGUI scoretxt;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        SoundEffects = GetComponent<AudioSource>();
        scoretxt.text = score.ToString();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collecter"))
        {
            Destroy(other.gameObject);
            SoundEffects.clip = Coins;
            SoundEffects.Play();
            score++;
            scoretxt.text = score.ToString();

        }
    }
}

