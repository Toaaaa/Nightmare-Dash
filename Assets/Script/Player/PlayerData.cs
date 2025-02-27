using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // ✅ 기본 스탯
    [Header("기본 스탯")]
    [SerializeField] private float ori_MaxHp = 100f; // 기본 체력
    [SerializeField] private float ori_score = 1f; // 기본 재화 획득량 (코인 획득 시 배율)
    [SerializeField] private float ori_InvincibleTime = 5f; // 기본 무적시간

    // ✅ 유물로 인한 추가 스탯
    [Header("유물 추가 스탯")]
    private float add_MaxHp = 0f;
    private float add_score = 0f;
    private float add_InvincibleTime = 0f;

    // ✅ 플레이어가 보유한 유물 & 펫 리스트
    [Header("보유 유물 및 펫 리스트")]
    public List<ArtifactData> OwnedArtifacts = new List<ArtifactData>();
    public List<PetData> OwnedPets = new List<PetData>(); // ✅ 펫 리스트 추가됨

    // ✅ 업적 리스트 추가
    [Header("업적 데이터")]
    public List<string> UnlockedAchievements = new List<string>();
    [Header("보유 재화")]
    public long Diamond;
    public long Coin;

    [Header("펫 정보")]
    // ✅ 현재 장착한 펫 정보 저장
    public string equippedPetID; // 펫 ID를 저장

    // ✅ 저장할 때 사용할 메서드
    public void EquipPet(PetData pet)
    {
        if (pet == null)
        {
            return;
        }
        equippedPetID = pet.Id.ToString();  // ✅ 선택한 펫의 ID 저장
        
        // ✅ 데이터를 저장 (파일, PlayerPrefs 등)
        GameManager.instance.SavePlayerData();
    }



    // ✅ 총 능력치 반환 (기본 + 추가)
    public float GetTotalHp() => ori_MaxHp + add_MaxHp;
    public float GetTotalScoreValue() => ori_score + add_score;
    public float GetTotalInvincibleTime() => ori_InvincibleTime + add_InvincibleTime;

    // ✅ 업적 해금
    public void UnlockAchievement(string achievementName)
    {
        if (!UnlockedAchievements.Contains(achievementName))
        {
            UnlockedAchievements.Add(achievementName);
            SavePlayerData();
        }
    }

    public bool IsAchievementUnlocked(string achievementName)
    {
        return UnlockedAchievements.Contains(achievementName);
    }

    // ✅ 유물 효과 적용
    private void ApplyArtifactEffect(ArtifactData artifact)
    {
        add_MaxHp += artifact.Effect.Hp;
        add_score += artifact.Effect.Currency;
        add_InvincibleTime += artifact.Effect.Invincibility;
    }

    // ✅ 유물 획득 및 적용
    public void AddArtifact(ArtifactData artifact)
    {
        if (artifact == null)
        {
            return;
        }

        if (OwnedArtifacts.Exists(a => a.Id == artifact.Id))
        {
            HandleDuplicateArtifact(artifact);
            return;
        }

        OwnedArtifacts.Add(artifact);
        ApplyArtifactEffect(artifact);
        SavePlayerData();
    }

    // ✅ 중복 유물 처리 (강화 시스템 추가 가능)
    private void HandleDuplicateArtifact(ArtifactData artifact)
    {
        // 여기에 강화 시스템 로직 추가 가능 (예: 동일 유물 3개 모으면 등급 업)
    }

    // ✅ 펫 획득 (중복 방지)
    public void AddPet(PetData pet)
    {
        if (pet == null)
        {
            return;
        }

        if (OwnedPets.Exists(p => p.Id == pet.Id))
        {
            return;
        }

        OwnedPets.Add(pet);
        SavePlayerData();
    }

    // ✅ 유물 & 펫 효과 초기화 (게임 시작 시 호출)
    public void ResetAddStats()
    {
        add_MaxHp = 0;
        add_score = 0;
        add_InvincibleTime = 0;
        OwnedArtifacts.Clear();
        OwnedPets.Clear(); // ✅ 펫도 초기화
    }

    // ✅ 유물 & 펫 데이터 저장
    public void SavePlayerData()
    {
        PlayerPrefs.SetString("Coin", Coin.ToString());
        PlayerPrefs.SetString("Diamond", Diamond.ToString());
        List<ArtifactData> artifactIds = OwnedArtifacts;
        List<PetData> petIds = OwnedPets; // ✅ 펫도 저장

        string json = JsonConvert.SerializeObject(new PlayerSaveData(artifactIds, petIds));
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    // ✅ 유물 & 펫 데이터 불러오기
    public void LoadPlayerData(Artifacts artifactManager, Pet petManager)
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);

            OwnedArtifacts = saveData.ArtifactIds;
            OwnedPets = saveData.PetIds;
        }

        if (PlayerPrefs.HasKey("Coin"))
        {
            long.TryParse(PlayerPrefs.GetString("Coin"), out Coin);
        }
        else
        {
            Coin = 0;
        }

        if (PlayerPrefs.HasKey("Diamond"))
        {
            long.TryParse(PlayerPrefs.GetString("Diamond"), out Diamond);
        }
        else
        {
            Diamond = 0;
        }
    }
}

// ✅ 유물 & 펫 ID 저장을 위한 데이터 구조
[System.Serializable]
public class PlayerSaveData
{
    public List<ArtifactData> ArtifactIds;
    public List<PetData> PetIds;

    public PlayerSaveData(List<ArtifactData> artifactIds, List<PetData> petIds)
    {
        ArtifactIds = artifactIds;
        PetIds = petIds;
    }
}

// ✅ 업적 저장용 구조체
[System.Serializable]
public class AchievementSaveData
{
    public List<string> Archiev;
    public AchievementSaveData(List<string> archiev)
    {
        Archiev = archiev;
    }
}
