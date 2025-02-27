using System.Collections.Generic;
using UnityEngine;

public class PetSelect : MonoBehaviour
{
    public List<PetData> petList;

    private void Start()
    {
        InitializePetList();
    }

    private void InitializePetList()
    {
        Pet petManager = FindObjectOfType<Pet>();
        if (petManager == null)
        {
            Debug.LogError("🚨 PetManager를 찾을 수 없습니다!");
            return;
        }

        petList = petManager.Pets;
    }

    public PetData GetRandomPet()
    {
        if (petList == null || petList.Count == 0)
        {
            Debug.LogError("🚨 펫 리스트가 비어 있습니다!");
            return null;
        }

        int randomIndex = Random.Range(0, petList.Count);
        return petList[randomIndex];
    }
}
