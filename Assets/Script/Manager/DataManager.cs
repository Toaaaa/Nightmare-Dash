using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    // 펫 데이터 관리용 Dictionary
    private Dictionary<int, PetData> petDictionary = new Dictionary<int, PetData>();
    private Dictionary<int, ArtifactData> artifactDictionary = new Dictionary<int, ArtifactData>();

    public Pet PetManager { get; set; }
    public Artifacts ArtifactManager { get; set; }

    public Coin Coin = new();

    public Diamond Diamond = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializePetData()
    {
        if (PetManager == null || PetManager.Pets == null)
        {
            Debug.LogError("🚨 PetManager 또는 PetManager.Pets가 null입니다! 펫 데이터를 올바르게 설정하세요.");
            return;
        }

        foreach (var pet in PetManager.Pets)
        {
            if (!petDictionary.ContainsKey(pet.Id))
            {
                petDictionary.Add(pet.Id, pet);
            }
            else
            {
                Debug.LogWarning($"⚠️ 중복된 Pet ID 발견: {pet.Id}");
            }
        }

        Debug.Log($"✅ 초기화 완료: 펫 개수 {petDictionary.Count}, 유물 개수 {artifactDictionary.Count}");
    }

    public void InitializeArtifactData()
    {
        if (ArtifactManager == null || ArtifactManager.ArtifactsList == null)
        {
            Debug.LogError("🚨 ArtifactManager 또는 ArtifactsList가 null입니다! 유물 데이터를 올바르게 로드했는지 확인하세요.");
            return;
        }

        artifactDictionary.Clear(); // ✅ 기존 데이터를 클리어하여 중복 로드 방지

        foreach (var artifact in ArtifactManager.ArtifactsList)
        {
            if (!artifactDictionary.ContainsKey(artifact.Id))
            {
                artifactDictionary.Add(artifact.Id, artifact);
                Debug.Log($"✅ 유물 추가됨: ID {artifact.Id}, 이름: {artifact.Name}");
            }
            else
            {
                Debug.LogWarning($"⚠️ 중복된 Artifact ID 발견: {artifact.Id}");
            }
        }

        Debug.Log($"✅ 유물 데이터 초기화 완료! 총 {artifactDictionary.Count}개의 유물 로드됨.");
    }

    public void SetArtifactObtained(int artifactId, bool obtained)
    {
        if (artifactDictionary.Count == 0)
        {
            Debug.LogWarning("⚠️ artifactDictionary가 비어 있습니다. 다시 초기화합니다.");
            InitializeArtifactData();
        }

        if (artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact))
        {
            artifact.IsObtained = obtained;
            Debug.Log($"✅ Artifact with ID {artifactId} obtained status: {obtained}");

            ArtifactData artifactInList = ArtifactManager.ArtifactsList.Find(a => a.Id == artifactId);
            if (artifactInList != null)
            {
                artifactInList.IsObtained = obtained;
            }

            if (obtained && !GameManager.instance.playerData.OwnedArtifacts.Exists(a => a.Id == artifactId))
            {
                GameManager.instance.playerData.OwnedArtifacts.Add(artifact);
                GameManager.instance.SavePlayerData();
            }
        }
        else
        {
            Debug.LogError($"🚨 Artifact ID {artifactId} not found");
        }
    }

    public PetData GetPetById(int petId)
    {
        return petDictionary.TryGetValue(petId, out PetData pet) ? pet : null;
    }

    public ArtifactData GetArtifactById(int artifactId)
    {
        return artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact) ? artifact : null;
    }
}
