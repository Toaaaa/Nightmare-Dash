using System.Collections;
using System.Collections.Generic;
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

    // ✅ 총 능력치 반환 (기본 + 추가)
    public float GetTotalHp() => ori_MaxHp + add_MaxHp;
    public float GetTotalScoreValue() => ori_score + add_score;
    public float GetTotalInvincibleTime() => ori_InvincibleTime + add_InvincibleTime;

    // ✅ 유물 효과 적용
    private void ApplyArtifactEffect(ArtifactData artifact)
    {
        add_MaxHp += artifact.Effect.Hp;
        add_score += artifact.Effect.Currency;
        add_InvincibleTime += artifact.Effect.Invincibility;

        Debug.Log($"🔹 유물 효과 적용: 체력 +{artifact.Effect.Hp}, 재화 배율 +{artifact.Effect.Currency}, 무적시간 +{artifact.Effect.Invincibility}");
    }

    // ✅ 유물 획득 및 적용
    public void AddArtifact(ArtifactData artifact)
    {
        if (artifact == null)
        {
            Debug.LogError("🚨 유물이 null입니다!");
            return;
        }

        if (OwnedArtifacts.Exists(a => a.Id == artifact.Id))
        {
            HandleDuplicateArtifact(artifact);
            return;
        }

        OwnedArtifacts.Add(artifact);
        ApplyArtifactEffect(artifact);

        Debug.Log($"✅ 유물 획득: {artifact.Name} → 효과 적용 완료!");
        SavePlayerData();
    }

    // ✅ 중복 유물 처리 (강화 시스템 추가 가능)
    private void HandleDuplicateArtifact(ArtifactData artifact)
    {
        Debug.Log($"⚠️ 중복 유물 획득: {artifact.Name} → 추가 강화 또는 보상 지급 가능!");
        // 여기에 강화 시스템 로직 추가 가능 (예: 동일 유물 3개 모으면 등급 업)
    }

    // ✅ 펫 획득 (중복 방지)
    public void AddPet(PetData pet)
    {
        if (pet == null)
        {
            Debug.LogError("🚨 펫 데이터가 null입니다!");
            return;
        }

        if (OwnedPets.Exists(p => p.Id == pet.Id))
        {
            Debug.Log($"⚠️ 중복 펫 획득: {pet.PetName} → 추가 강화 가능!");
            return;
        }

        OwnedPets.Add(pet);
        Debug.Log($"🎉 플레이어가 '{pet.PetName}' 펫을 획득했습니다!");
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

        Debug.Log("🔄 유물 & 펫 효과 초기화 완료!");
    }

    // ✅ 유물 & 펫 데이터 저장
    public void SavePlayerData()
    {
        List<int> artifactIds = OwnedArtifacts.ConvertAll(a => a.Id);
        List<int> petIds = OwnedPets.ConvertAll(p => p.Id); // ✅ 펫도 저장

        string json = JsonUtility.ToJson(new PlayerSaveData(artifactIds, petIds));
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

            OwnedArtifacts = new List<ArtifactData>();
            OwnedPets = new List<PetData>();

            foreach (int id in saveData.ArtifactIds)
            {
                ArtifactData artifact = artifactManager.ArtifactsList.Find(a => a.Id == id);
                if (artifact != null)
                {
                    artifact.IsObtained = true;
                    OwnedArtifacts.Add(artifact);
                    ApplyArtifactEffect(artifact);
                }
            }

            foreach (int id in saveData.PetIds)
            {
                PetData pet = petManager.Pets.Find(p => p.Id == id);
                if (pet != null)
                {
                    pet.IsObtained = true;
                    OwnedPets.Add(pet);
                }
            }

            Debug.Log($"✅ 플레이어 데이터 불러오기 완료! 보유 유물: {OwnedArtifacts.Count}, 보유 펫: {OwnedPets.Count}");
        }
    }
}

// ✅ 유물 & 펫 ID 저장을 위한 데이터 구조
[System.Serializable]
public class PlayerSaveData
{
    public List<int> ArtifactIds;
    public List<int> PetIds;

    public PlayerSaveData(List<int> artifactIds, List<int> petIds)
    {
        ArtifactIds = artifactIds;
        PetIds = petIds;
    }
}
