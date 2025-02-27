using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetData
{
    public int Id { get; set; }
    public string PetName { get; set; }
    public string PetDescription { get; set; }
    public bool IsObtained { get; set; }
    public Sprite PetImage { get; set; } // ✅ 펫 이미지 추가
}

public class Pet : MonoBehaviour
{
    public List<PetData> Pets = new List<PetData>(); // ✅ Pets 리스트 초기화
    public Sprite[] PetImages;  // ✅ 펫 이미지를 저장할 배열

    void Awake()
    {
        if (FindObjectsOfType<Pet>().Length > 1)
        {
            Destroy(gameObject); // ✅ 중복 방지
            return;
        }

        DontDestroyOnLoad(gameObject); // ✅ 씬 변경 시 유지

        if (Pets == null)
        {
            Pets = new List<PetData>();
        }
    }

    void Start()
    {
        // ✅ Resources에서 펫 이미지 불러오기
        PetImages = Resources.LoadAll<Sprite>("pet");

        if (PetImages == null || PetImages.Length == 0)
        {
            return;
        }

        // ✅ 이미지 이름 기준으로 정렬 (Pets_1, Pets_2 순서대로)
        Array.Sort(PetImages, (a, b) => a.name.CompareTo(b.name));

        // ✅ 펫 데이터 초기화
        InitializePets();
    }

    void InitializePets()
    {
        if (Pets == null)
        {
            Pets = new List<PetData>();
        }

        Pets.Clear(); // ✅ 기존 데이터 제거 후 새로 추가

        Pets.Add(new PetData { Id = 1, PetName = "보라 개구리 1", PetDescription = "작은 보라 개구리", IsObtained = false, PetImage = GetPetImage(0) });
        Pets.Add(new PetData { Id = 2, PetName = "보라 개구리 2", PetDescription = "큰 보라 개구리", IsObtained = false, PetImage = GetPetImage(1) });
        Pets.Add(new PetData { Id = 3, PetName = "파란 개구리 1", PetDescription = "작은 파란 개구리", IsObtained = false, PetImage = GetPetImage(2) });
        Pets.Add(new PetData { Id = 4, PetName = "파란 개구리 2", PetDescription = "큰 파란 개구리", IsObtained = false, PetImage = GetPetImage(3) });
        Pets.Add(new PetData { Id = 5, PetName = "녹색 개구리 1", PetDescription = "작은 녹색 개구리", IsObtained = false, PetImage = GetPetImage(4) });
        Pets.Add(new PetData { Id = 6, PetName = "녹색 개구리 2", PetDescription = "큰 녹색 개구리", IsObtained = false, PetImage = GetPetImage(5) });

    }

    // ✅ 펫 이미지 가져오기 (배열 크기를 넘어가면 null 반환)
    private Sprite GetPetImage(int index)
    {
        if (PetImages != null && index < PetImages.Length)
        {
            return PetImages[index];
        }
        return null;
    }
}
