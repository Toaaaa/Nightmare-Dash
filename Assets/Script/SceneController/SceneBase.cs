using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    public static SceneBase Current { get; private set; } = null;

    private static object m_SceneBridgeData;

    public static string SceneName;

    private void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        OnStart(m_SceneBridgeData);
    }

    protected virtual void OnStart(object data)
    {
        SceneName = SceneManager.GetActiveScene().name;
    }

    public static void LoadScene(string sceneName, object data = null)
    {
        m_SceneBridgeData = data;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
