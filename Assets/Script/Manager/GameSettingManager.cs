using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    private static GameSettingsManager instance;

    public static GameSettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GameSettingsManager");
                instance = obj.AddComponent<GameSettingsManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private float volume = 1f;
    private bool isMuted = false;

    private void Awake()
    {
        LoadSettings();
    }

    public float Volume
    {
        get => isMuted ? 0 : volume;
        set
        {
            volume = value;
            SaveSettings();
            UpdateAudio();
        }
    }

    public bool IsMuted
    {
        get => isMuted;
        set
        {
            isMuted = value;
            SaveSettings();
            UpdateAudio();
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void LoadSettings()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
    }

    private void UpdateAudio()
    {
        AudioListener.volume = Volume;
    }
}
