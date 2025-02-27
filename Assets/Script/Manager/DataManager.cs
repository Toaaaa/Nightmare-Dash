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
        if (PetManager != null)
        {
            
        }

        // ✅ ArtifactManager 강제 찾기
        ArtifactManager = FindObjectOfType<Artifacts>();
        InitializePetData();
        InitializeArtifactData();
    }

    // ✅ 펫 데이터 초기화
    public void InitializePetData()
    {
        if (PetManager == null)
        {
            PetManager = FindObjectOfType<Pet>();

            if (PetManager == null)
            {
                return;
            }
        }

        if (PetManager.Pets == null)
        {
            PetManager.Pets = new List<PetData>();
        }

        foreach (var pet in PetManager.Pets)
        {
            if (!petDictionary.ContainsKey(pet.Id))
            {
                petDictionary.Add(pet.Id, pet);
            }
        }

        
    }

    // ✅ 유물 데이터 초기화
    public void InitializeArtifactData()
    {
        if (ArtifactManager == null || ArtifactManager.ArtifactsList == null)
        {
            return;
        }

        artifactDictionary.Clear(); // ✅ 기존 데이터 클리어

        foreach (var artifact in ArtifactManager.ArtifactsList)
        {
            if (!artifactDictionary.ContainsKey(artifact.Id))
            {
                artifactDictionary.Add(artifact.Id, artifact);
            }
        }

        
    }

    // ✅ 유물 획득 처리
    public void SetArtifactObtained(int artifactId, bool obtained)
    {
        if (artifactDictionary.Count == 0)
        {
            InitializeArtifactData();
        }

        if (artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact))
        {
            artifact.IsObtained = obtained;

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
    }

    // ✅ 펫 획득 처리
    public void SetPetObtained(int petId, bool obtained)
    {
        if (PetManager == null || PetManager.Pets == null)
        {
            return;
        }

        if (petDictionary.Count == 0)
        {
            InitializePetData();
        }

        if (petDictionary.TryGetValue(petId, out PetData pet))
        {
            pet.IsObtained = obtained;

            PetData petInList = PetManager.Pets.Find(p => p.Id == petId);
            if (petInList != null)
            {
                petInList.IsObtained = obtained;
            }

            if (obtained && GameManager.instance != null)
            {
                if (!GameManager.instance.playerData.OwnedPets.Exists(p => p.Id == petId))
                {
                    GameManager.instance.playerData.OwnedPets.Add(pet);
                    GameManager.instance.SavePlayerData();
                }
            }
        }
    }

    // ✅ 펫 ID로 펫 반환
    public PetData GetPetById(int petId)
    {
        return petDictionary.TryGetValue(petId, out PetData pet) ? pet : null;
    }

    // ✅ 유물 ID로 유물 반환
    public ArtifactData GetArtifactById(int artifactId)
    {
        return artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact) ? artifact : null;
    }
}
