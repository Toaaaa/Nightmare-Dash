using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GachaManager : MonoBehaviour
{
    public GameObject HidePrefabs;
    public GameObject[] DrawPrefabs;
    // Start is called before the first frame update
    public void StartDraw(int num)
    {
        if (num == 1)
        {
            HidePrefabs.SetActive(true);
        }
        else
        {
        }
    }
    
        
    
}
