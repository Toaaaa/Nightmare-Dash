using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaUI : MonoBehaviour
{
    [SerializeField] private Button gatchaButton;

    private void Start()
    {
        gatchaButton.onClick.AddListener(OnGatchaClick);
    }

    private void OnGatchaClick()
    {
        SceneManager.LoadScene("Gacha");
    }

}
