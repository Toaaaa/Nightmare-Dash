using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // ✅ 펫 & 유물 데이터 관리용 Dictionary
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

        // ✅ PetManager 강제 찾기
        PetManager = FindObjectOfType<Pet>();
        if (PetManager == null)
        {
            Debug.LogError("🚨 PetManager를 찾을 수 없습니다! Pet 오브젝트가 Hierarchy에 존재하는지 확인하세요.");
        }
        else
        {
            Debug.Log($"✅ PetManager가 정상적으로 할당됨: {PetManager.name}");
        }

        // ✅ ArtifactManager 강제 찾기
        ArtifactManager = FindObjectOfType<Artifacts>();
        if (ArtifactManager == null)
        {
            Debug.LogError("🚨 ArtifactManager를 찾을 수 없습니다!");
        }

        InitializePetData();
        InitializeArtifactData();
    }

    public void InitializePetData()
    {
        if (PetManager == null)
        {
            Debug.LogError("🚨 PetManager가 null입니다! PetManager를 찾을 수 없습니다.");
            PetManager = FindObjectOfType<Pet>();

            if (PetManager == null)
            {
                Debug.LogError("🚨 Pet 오브젝트가 씬에 없습니다! Hierarchy에 추가하세요.");
                return;
            }
        }

        if (PetManager.Pets == null)
        {
            Debug.LogWarning("⚠️ PetManager.Pets 리스트가 null이어서 강제 초기화합니다.");
            PetManager.Pets = new List<PetData>();
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

        artifactDictionary.Clear(); // ✅ 기존 데이터 클리어

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

    // ✅ 유물 획득 처리
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
            Debug.Log($"🎁 플레이어가 '{artifact.Name}' 유물을 획득했습니다!");

            ArtifactData artifactInList = ArtifactManager.ArtifactsList.Find(a => a.Id == artifactId);
            if (artifactInList != null)
            {
                artifactInList.IsObtained = obtained;
            }

            if (obtained && GameManager.instance != null && !GameManager.instance.playerData.OwnedArtifacts.Exists(a => a.Id == artifactId))
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

    // ✅ 펫 획득 처리
    public void SetPetObtained(int petId, bool obtained)
    {
        if (PetManager == null)
        {
            Debug.LogError("🚨 PetManager가 null입니다! PetManager를 찾을 수 없습니다.");
            return;
        }

        if (PetManager.Pets == null)
        {
            Debug.LogError("🚨 PetManager.Pets가 null입니다! PetManager의 Start()에서 Pets 리스트가 초기화되었는지 확인하세요.");
            return;
        }

        if (petDictionary.Count == 0)
        {
            Debug.LogWarning("⚠️ petDictionary가 비어 있습니다. 다시 초기화합니다.");
            InitializePetData();
        }

        if (!petDictionary.TryGetValue(petId, out PetData pet))
        {
            Debug.LogError($"🚨 Pet ID {petId} not found in petDictionary");
            return;
        }

        pet.IsObtained = obtained;
        Debug.Log($"🎁 플레이어가 '{pet.PetName}' 펫을 획득했습니다!");

        PetData petInList = PetManager.Pets.Find(p => p.Id == petId);
        if (petInList != null)
        {
            petInList.IsObtained = obtained;
        }

        // ✅ 중복 방지 후 플레이어 데이터에 추가
        if (obtained && GameManager.instance != null)
        {
            if (!GameManager.instance.playerData.OwnedPets.Exists(p => p.Id == petId))
            {
                GameManager.instance.playerData.OwnedPets.Add(pet);
                Debug.Log($"✅ '{pet.PetName}' 펫이 PlayerData에 추가됨!");
                GameManager.instance.SavePlayerData(); // ✅ 데이터 저장
            }
            else
            {
                Debug.Log($"⚠️ '{pet.PetName}' 펫은 이미 보유 중입니다.");
            }
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
