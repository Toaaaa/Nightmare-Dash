using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Modules.Util
{
    public class TextFontEditor
    {
        //폰트 경로 설정.
        public const string PATH_FONT_UITEXT_Bon = "Assets/Font/bon.ttf";
        public const string PATH_FONT_TEXTMESHPRO_Bon = "Assets/Font/bon.asset";

        [MenuItem("CustomMenu/ChangeUITextFont(현재 Scene 내 UIText 폰트를 Bon 폰트로 교체함)")]
        public static void ChangeFontInUIText()
        {
            GameObject[] rootObj = GetSceneRootObjects();

            for (int i = 0; i < rootObj.Length; i++)
            {
                GameObject gbj = (GameObject)rootObj[i] as GameObject;
                Component[] com = gbj.transform.GetComponentsInChildren(typeof(Text), true);
                foreach (Text txt in com)
                {
                    txt.font = AssetDatabase.LoadAssetAtPath<Font>(PATH_FONT_UITEXT_Bon);
                }
            }
        }

        [MenuItem("CustomMenu/ChangeTextMeshPro(현재 Scene 내 TextMeshProUGUI 폰트를 Bon 폰트로 교체함)")]
        public static void ChangeFontInTextMeshPro()
        {
            GameObject[] rootObj = GetSceneRootObjects();

            for (int i = 0; i < rootObj.Length; i++)
            {
                GameObject gbj = (GameObject)rootObj[i] as GameObject;
                Component[] com = gbj.transform.GetComponentsInChildren(typeof(TextMeshProUGUI), true);
                foreach (TextMeshProUGUI txt in com)
                {
                    txt.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(PATH_FONT_TEXTMESHPRO_Bon);
                }
            }
        }

        /// <summary>
        /// 모든 최상위 Root의 GameObject를 받아옴.
        /// </summary>
        /// <returns></returns>
        private static GameObject[] GetSceneRootObjects()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            return currentScene.GetRootGameObjects();
        }
    }
}