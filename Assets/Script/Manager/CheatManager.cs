using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        // 다이아 추가 치트키
        if (Input.GetKeyDown(KeyCode.D))
        {
            DataManager.Instance.Diamond.Add(1000);
        }
        // 코인 추가 치트키
        if (Input.GetKeyDown(KeyCode.C))
        {
            DataManager.Instance.Coin.Add(1000);
        }
#endif
    }
}
