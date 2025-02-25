using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet
{
    public string id; // 펫의 고유 ID
    public string petName;
    public string petDescription;
}

public class Pets : MonoBehaviour
{
    // 펫 데이터 리스트
    public List<Pet> pets = new List<Pet>();

    void Start()
    {
        // 펫 데이터 초기화
        InitializePets();

        // 펫 출력 (디버깅용)
        foreach (var pet in pets)
        {
            Debug.Log($"Pet ID: {pet.id}, Name: {pet.petName}, Description: {pet.petDescription}");
        }
    }

    // 펫 데이터 초기화 메서드
    void InitializePets()
    {
        pets.Add(new Pet
        {
            id = "pet_001", // 고유 ID 추가
            petName = "부끄러운 고양이",
            petDescription = "조용하고 부끄러움을 타는 고양이입니다."
        });

        pets.Add(new Pet
        {
            id = "pet_002", // 고유 ID 추가
            petName = "용감한 개",
            petDescription = "모든 상황에서 용감하고 충성스러운 개입니다."
        });

        pets.Add(new Pet
        {
            id = "pet_003", // 고유 ID 추가
            petName = "귀여운 토끼",
            petDescription = "활발하고 사랑스러운 토끼입니다."
        });
    }

    // 랜덤으로 펫 하나를 뽑는 메서드
    public Pet GetRandomPet()
    {
        if (pets.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, pets.Count);
            return pets[randomIndex];
        }
        return null;
    }

    // ID로 펫을 찾는 메서드
    public Pet GetPetById(string id)
    {
        return pets.Find(pet => pet.id == id);
    }
}
