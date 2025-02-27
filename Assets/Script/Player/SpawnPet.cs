using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPet : MonoBehaviour
{
    public GameObject petPrefab; // 펫 프리팹을 연결할 변수

    void Start()
    {
        LoadEquippedPet();
    }


    public void LoadEquippedPet()
    {
        Debug.Log("작동중");
        string savedPetID = GameManager.instance.playerData.equippedPetID;

        if (!string.IsNullOrEmpty(savedPetID))
        {
            // 저장된 펫 ID로 PetData 검색
            PetData equippedPet = GameManager.instance.playerData.OwnedPets.Find(p => p.Id.ToString() == savedPetID);

            if (equippedPet != null)
            {
                PlayerCustomUI.Instance.SpawnPet(equippedPet.PetImage); // 펫 생성
                Debug.Log($"저장된 펫 로드됨: {equippedPet.PetName}");
            }
            else
            {
                Debug.LogWarning("저장된 펫을 찾을 수 없습니다!");
            }
        }
    }
}
