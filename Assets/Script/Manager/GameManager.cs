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
            PlayerCustomUI.Instance?.LoadEquippedPet(); // 새로운 씬에서 펫 다시 생성

            // ✅ DataManager 인스턴스 저장
            dataManager = FindObjectOfType<DataManager>();
            if (dataManager == null)
            {
                return;
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
            playerData = Resources.Load<PlayerData>("PlayerData");
        }

        if (playerData == null)
        {
            return;
        }
        else
        {
            Artifacts artifactManager = FindObjectOfType<Artifacts>();
            Pet petManager = FindObjectOfType<Pet>(); // ✅ 펫 데이터 로드 추가

            if (artifactManager == null)
            {
                return;
            }
            else if (petManager == null)
            {
                return;
            }
            else
            {
                playerData.LoadPlayerData(artifactManager, petManager);
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
            return;
        }
    }

    // ✅ DataManager를 통해 SetPetObtained 호출하는 메서드 추가
    public void SetPetObtained(int petId, bool obtained)
    {
        if (dataManager != null)
        {
            dataManager.SetPetObtained(petId, obtained);
        }
        else
        {
            return;
        }
    }

    // ✅ 플레이어가 가진 유물 리스트 출력 (디버깅용)
    public void PrintPlayerArtifacts()
    {
        if (playerData == null)
        {
            return;
        }

        foreach (var artifact in playerData.OwnedArtifacts)
        {
        }
    }

    // ✅ 플레이어가 가진 펫 리스트 출력 (디버깅용)
    public void PrintPlayerPets()
    {
        if (playerData == null)
        {
            return;
        }

        foreach (var pet in playerData.OwnedPets)
        {
        }
    }

    // ✅ 플레이어 데이터 저장 기능 (PlayerPrefs)
    public void SavePlayerData()
    {
        if (playerData == null)
        {
            return;
        }

        playerData.SavePlayerData();
    }
}
