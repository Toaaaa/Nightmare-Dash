using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSound : MonoBehaviour
{
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource itemSound;

    public void PlayCoinSound()
    {
        coinSound.Play();
    }
    public void PlayItemSound()
    {
        itemSound.Play();
    }
}
