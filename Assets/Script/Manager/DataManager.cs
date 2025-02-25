using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 펫 데이터 관리용 Dictionary
    private Dictionary<int, PetData> petDictionary = new Dictionary<int, PetData>();

    // 유물 데이터 관리용 Dictionary
    private Dictionary<int, ArtifactData> artifactDictionary = new Dictionary<int, ArtifactData>();

    public Pet PetManager { get; set; } // 펫 참조
    public Artifact ArtifactManager { get; set; } // 유물 참조

    void Start()
    {
        // 펫과 유물 데이터 초기화
        InitializePetData();
        InitializeArtifactData();

        // 예시: 특정 펫을 획득했을 때, 해당 펫의 IsObtained 값을 변경
        SetPetObtained(1, true); // 펫 ID 1번을 얻었음
        SetArtifactObtained(1, true); // 유물 ID 1번을 얻었음
    }

    // 펫 데이터 초기화
    void InitializePetData()
    {
        foreach (var pet in PetManager.Pets)
        {
            petDictionary.Add(pet.Id, pet);
        }
    }

    // 유물 데이터 초기화
    void InitializeArtifactData()
    {
        foreach (var artifact in ArtifactManager.ArtifactsList)
        {
            artifactDictionary.Add(artifact.Id, artifact);
        }
    }

    // 펫을 얻었는지 여부 설정 (ID로 찾고, 획득 여부 변경)
    public void SetPetObtained(int petId, bool obtained)
    {
        if (petDictionary.TryGetValue(petId, out PetData pet))
        {
            pet.IsObtained = obtained;
            Debug.Log($"Pet with ID {petId} obtained status: {obtained}");
        }
        else
        {
            Debug.LogError($"Pet ID {petId} not found");
        }
    }

    // 유물을 얻었는지 여부 설정 (ID로 찾고, 획득 여부 변경)
    public void SetArtifactObtained(int artifactId, bool obtained)
    {
        if (artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact))
        {
            artifact.IsObtained = obtained;
            Debug.Log($"Artifact with ID {artifactId} obtained status: {obtained}");
        }
        else
        {
            Debug.LogError($"Artifact ID {artifactId} not found");
        }
    }

    // 특정 펫을 ID로 찾기
    public PetData GetPetById(int petId)
    {
        return petDictionary.TryGetValue(petId, out PetData pet) ? pet : null;
    }

    // 특정 유물을 ID로 찾기
    public ArtifactData GetArtifactById(int artifactId)
    {
        return artifactDictionary.TryGetValue(artifactId, out ArtifactData artifact) ? artifact : null;
    }
}
