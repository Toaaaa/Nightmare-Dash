using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GachaSceneController : SceneBase
{
    private DataManager dataManager;

    protected override void OnStart(object data)
    {
        base.OnStart(data);

        dataManager = DataManager.Instance;

        // ✅ ArtifactManager를 강제로 찾고 할당
        if (dataManager.ArtifactManager == null)
        {
            dataManager.ArtifactManager = FindObjectOfType<Artifacts>();
            if (dataManager.ArtifactManager == null)
            {
                Debug.LogError("🚨 ArtifactManager를 찾을 수 없습니다! 씬에 존재하는지 확인하세요.");
                return;
            }
        }

        // ✅ 유물 데이터 즉시 초기화
        dataManager.InitializeArtifactData();

        dataManager.InitializePetData();
        // 예제 실행
        //SetPetObtained(1, true);
        dataManager.SetArtifactObtained(1, true);
    }



}
