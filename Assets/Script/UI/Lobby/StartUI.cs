using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClick);
    }

    private void OnStartClick()
    {
        
        SceneManager.LoadScene("Game");

    }

}