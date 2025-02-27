using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbySceneController : SceneBase
{
    protected override void OnStart(object data)
    {
        base.OnStart(data);

        
        // 씬 전환 후 플레이어 데이터 로드
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadPlayerData();
        }
        else
        {
            Debug.LogError("GameManager가 존재하지 않습니다. 데이터 로드 실패");
        }

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
}
