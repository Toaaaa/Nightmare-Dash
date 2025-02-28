using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbySceneController : SceneBase
{

    public PetData EquipPetImage = null;

    protected override void OnStart(object data)
    {
        base.OnStart(data);

        // 유물 슬롯 UI 갱신
        PlayerCustomUI playerCustomUI = FindObjectOfType<PlayerCustomUI>();
        if (playerCustomUI != null)
        {
            playerCustomUI.LoadArtifactSlots();
            playerCustomUI.LoadPetSlots();
            playerCustomUI.LoadEquippedPet();
        }
        else
        {
            Debug.LogWarning("PlayerCustomUI가 씬에 존재하지 않습니다.");
        }
    }

    public void PlayGame()
    {
        if (EquipPetImage == null)
        {
            Debug.Log("펫 이미지를 설정해주세요!");
            return;
        }
        LoadScene("Game", EquipPetImage);
    }
}
