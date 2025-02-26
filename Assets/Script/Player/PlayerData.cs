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

    // ✅ 플레이어가 보유한 유물 리스트
    [Header("보유 유물 리스트")]
    public List<ArtifactData> OwnedArtifacts = new List<ArtifactData>();

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

        // 중복 유물인지 확인
        if (OwnedArtifacts.Exists(a => a.Id == artifact.Id))
        {
            HandleDuplicateArtifact(artifact);
            return;
        }

        // 유물 추가
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

    // ✅ 유물 효과 초기화 (게임 시작 시 호출)
    public void ResetAddStats()
    {
        add_MaxHp = 0;
        add_score = 0;
        add_InvincibleTime = 0;
        OwnedArtifacts.Clear();

        Debug.Log("🔄 유물 효과 초기화 완료!");
    }

    // ✅ 유물 데이터 저장
    public void SavePlayerData()
    {
        List<int> artifactIds = OwnedArtifacts.ConvertAll(a => a.Id);
        string json = JsonUtility.ToJson(new ArtifactSaveData(artifactIds));
        PlayerPrefs.SetString("PlayerArtifacts", json);
        PlayerPrefs.Save();
    }

    // ✅ 유물 데이터 불러오기
    public void LoadPlayerData(Artifacts artifactManager)
    {
        if (PlayerPrefs.HasKey("PlayerArtifacts"))
        {
            string json = PlayerPrefs.GetString("PlayerArtifacts");
            ArtifactSaveData saveData = JsonUtility.FromJson<ArtifactSaveData>(json);
            OwnedArtifacts = new List<ArtifactData>();

            foreach (int id in saveData.Ids)
            {
                ArtifactData artifact = artifactManager.ArtifactsList.Find(a => a.Id == id);
                if (artifact != null)
                {
                    OwnedArtifacts.Add(artifact);
                    ApplyArtifactEffect(artifact);
                }
            }
            Debug.Log($"✅ 플레이어 유물 불러오기 완료! 보유 유물 개수: {OwnedArtifacts.Count}");
        }
    }
}

// ✅ 유물 ID 저장을 위한 데이터 구조
[System.Serializable]
public class ArtifactSaveData
{
    public List<int> Ids;
    public ArtifactSaveData(List<int> ids)
    {
        Ids = ids;
    }
}
