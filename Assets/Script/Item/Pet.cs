using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetData
{
    public int Id { get; set; } 
    public string PetName { get; set; }
    public string PetDescription { get; set; }
}

public class Pet : MonoBehaviour
{
    // 펫 데이터 리스트
    public List<PetData> Pets = new List<PetData>();

    void Start()
    {
        // 펫 데이터 초기화
        InitializePets();

        // 펫 출력 (디버깅용)
        foreach (var pet in Pets)
        {
            Debug.Log($"Pet ID: {pet.Id}, Name: {pet.PetName}, Description: {pet.PetDescription}");
        }
    }

    // 펫 데이터 초기화 메서드
    void InitializePets()
    {
        Pets.Add(new PetData
        {
            Id = 1, 
            PetName = "부끄러운 고양이",
            PetDescription = "조용하고 부끄러움을 타는 고양이입니다."
        });

        Pets.Add(new PetData
        {
            Id = 2, 
            PetName = "용감한 개",
            PetDescription = "모든 상황에서 용감하고 충성스러운 개입니다."
        });

        Pets.Add(new PetData
        {
            Id = 3, 
            PetName = "귀여운 토끼",
            PetDescription = "활발하고 사랑스러운 토끼입니다."
        });
    }

    // 랜덤으로 펫 하나를 뽑는 메서드
    public PetData GetRandomPet()
    {
        if (Pets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, Pets.Count);
            return Pets[randomIndex];
        }
        return null;
    }

    // ID로 펫을 찾는 메서드
    public PetData GetPetById(int Id)  
    {
        return Pets.Find(pet => pet.Id == Id);
    }
}
