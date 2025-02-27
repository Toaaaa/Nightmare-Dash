using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSelect : MonoBehaviour
{
    public List<PetData> petList;  // 펫 데이터 리스트

    private void Start()
    {
        // 초기화
        StartCoroutine(WaitForPetManager());
    }

    // PetManager가 로드될 때까지 대기
    private IEnumerator WaitForPetManager()
    {
        while (FindObjectOfType<Pet>() == null)
        {
            yield return null;
        }

        Pet petManager = FindObjectOfType<Pet>();
        if (petManager != null)
        {
            petList = petManager.Pets;
        }

    }

    // 랜덤 펫 선택
    public PetData GetRandomPet()
    {
        if (petList.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, petList.Count);
        return petList[randomIndex];
    }
}
