using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSound : MonoBehaviour
{
    [SerializeField] AudioSource coinSound;

    public void PlayCoinSound()
    {
        coinSound.Play();
    }
}
