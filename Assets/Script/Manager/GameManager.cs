using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerData playerData; // ✅ PlayerData를 참조
    private DataManager dataManager; // ✅ DataManager 인스턴스 저장

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // ✅ Resources에서 PlayerData 자동 로드
            LoadPlayerData();

            // ✅ DataManager 인스턴스 저장
            dataManager = FindObjectOfType<DataManager>();
            if (dataManager == null)
            {
                Debug.LogError("🚨 DataManager를 찾을 수 없습니다!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ✅ 플레이어 데이터 불러오기 기능 (게임 시작 시 호출)
    public void LoadPlayerData()
    {
        if (playerData == null)
        {
            Debug.LogWarning("⚠️ PlayerData가 null입니다. Resources에서 다시 로드합니다.");
            playerData = Resources.Load<PlayerData>("PlayerData");
        }

        if (playerData == null)
        {
            Debug.LogError("🚨 PlayerData를 불러오지 못했습니다! Resources 폴더를 확인하세요.");
        }
        else
        {
            Debug.Log("✅ PlayerData 로드 성공!");
            Artifacts artifactManager = FindObjectOfType<Artifacts>();

            if (artifactManager == null)
            {
                Debug.LogError("🚨 Artifacts Manager를 찾을 수 없습니다!");
            }
            else
            {
                playerData.LoadPlayerData(artifactManager);
                Debug.Log("✅ 플레이어 데이터 불러오기 완료!");
            }
        }
    }

    // ✅ DataManager를 통해 SetArtifactObtained 호출하는 메서드 추가
    public void SetArtifactObtained(int artifactId, bool obtained)
    {
        if (dataManager != null)
        {
            dataManager.SetArtifactObtained(artifactId, obtained);
        }
        else
        {
            Debug.LogError("🚨 DataManager 인스턴스가 설정되지 않았습니다!");
        }
    }

    // ✅ 플레이어가 가진 유물 리스트 출력 (디버깅용)
    public void PrintPlayerArtifacts()
    {
        if (playerData == null)
        {
            Debug.LogError("🚨 PlayerData가 없습니다!");
            return;
        }

        Debug.Log($"🎒 플레이어가 보유한 유물 개수: {playerData.OwnedArtifacts.Count}");
        foreach (var artifact in playerData.OwnedArtifacts)
        {
            Debug.Log($"🔹 유물: {artifact.Name} (효과: {artifact.GetEffectDescription()})");
        }
    }

    // ✅ 플레이어 데이터 저장 기능 (PlayerPrefs)
    public void SavePlayerData()
    {
        if (playerData == null)
        {
            Debug.LogError("🚨 PlayerData가 없습니다! 저장할 수 없습니다.");
            return;
        }

        playerData.SavePlayerData();
        Debug.Log("✅ 플레이어 데이터 저장 완료!");
    }
}
