using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button DrawOneBtn;  // "Draw One" ��ư
    public Button DrawTenBtn;  // "Draw Ten" ��ư
    public Image GachaFadeBlack; // ������ ���̵� ȭ��
    public float fadeDuration = 0.5f; // ���̵� ȿ�� ���� �ð�
    private float maxAlpha = 0.5f; // �ִ� ���� (0 = ���� ����, 1 = ���� ������)

    private void Start()
    {
        // ��ư Ŭ�� �� Gacha ���� ����
        DrawOneBtn.onClick.AddListener(() => StartCoroutine(GachaFadeEffect()));
        DrawTenBtn.onClick.AddListener(() => StartCoroutine(GachaFadeEffect()));
    }

    // ���̵� ��/�ƿ� ȿ�� (������ ����)
    private IEnumerator GachaFadeEffect()
    {
        // 1. ȭ���� �������ϰ� ���� (���̵� ��)
        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0); // �����ϰ� ����
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 2. ��í ���� �� (������ ���� ����)
        GachaFadeBlack.color = new Color(0, 0, 0, maxAlpha);
        yield return new WaitForSeconds(1.0f); // ��í ���� �ð� (1�� ���� ����)

        // 3. ȭ���� �ٽ� �����ϰ� ����� (���̵� �ƿ�)
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(maxAlpha, 0, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        GachaFadeBlack.gameObject.SetActive(false); // ���̵� �ƿ� �� ��Ȱ��ȭ
    }
}
