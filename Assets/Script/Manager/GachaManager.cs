using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Button DrawOneBtn;  // "Draw One" ��ư
    public Button DrawTenBtn;  // "Draw Ten" ��ư
    public Button ExitBtn;     // "Exit" ��ư (������ ��ư)
    public Image GachaFadeBlack; // ������ ���̵� ȭ��
    public float fadeDuration = 1.0f; // ���̵� ȿ�� ���� �ð�
    private float maxAlpha = 0.5f; // �ִ� ����
    public GameObject card;  // ������ ���� �����ϴ� ī�� ������Ʈ (Inspector���� �Ҵ� �ʿ�)

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawTenBtn.onClick.AddListener(() => DrawOneBtnClick(10));
        ExitBtn.onClick.AddListener(HideCardAndFadeBlack); // ������ ��ư �̺�Ʈ ����

        // Exit ��ư�� ó���� ��Ȱ��ȭ
        ExitBtn.gameObject.SetActive(false);

        // �����: ī�尡 �Ҵ�Ǿ����� Ȯ��
        if (card == null)
        {
            Debug.LogError("card is NOT assigned! Assign the card object in the Inspector.");
        }
        else
        {
            card.SetActive(false); // ���� ���� �� ��Ȱ��ȭ
            Debug.Log("Card is assigned correctly: " + card.name);
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void DrawOneBtnClick(int num)
    {
        StartCoroutine(FadeInEffect(num));
    }

    // Fade in ȿ�� + ī�� Ȱ��ȭ
    private IEnumerator FadeInEffect(int num)
    {
        // ī��� Fade Black �ʱ�ȭ
        ResetCardRotation(); // ī�� ȸ�� ����

        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0); // ������ ���� ����
        float elapsedTime = 0f;

        // ���̵� �� ȿ�� (��� ����)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ī�� Ȱ��ȭ (ī�嵵 ���� õõ�� ���̵��� ����)
        if (num == 1)
        {
            if (card != null)
            {
                StartCoroutine(ShowCardSlowly());  // ī�� õõ�� ���̵��� ó��
                Debug.Log("Card is now visible: " + card.name);
            }
            else
            {
                Debug.LogError("Card object is null! Make sure it's assigned in the Inspector.");
            }
        }
        else
        {
            Debug.Log("10�� �̱� ������ �ʿ���!");
        }
    }

    // ī�尡 õõ�� ���̵��� �ϴ� �޼���
    private IEnumerator ShowCardSlowly()
    {
        float cardAlpha = 0f;
        card.SetActive(true); // ī�� Ȱ��ȭ

        // ī�尡 ���������� ���̵��� ó��
        CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
        if (cardCanvasGroup == null)
        {
            cardCanvasGroup = card.AddComponent<CanvasGroup>();  // CanvasGroup�� ������ �߰�
        }

        // ī���� ���� ���� ���������� �������Ѽ� ���̰� ����
        while (cardAlpha < 1f)
        {
            cardAlpha += Time.deltaTime / fadeDuration; // fadeDuration�� ���� õõ�� ��ȭ
            cardCanvasGroup.alpha = Mathf.Lerp(0, 1, cardAlpha);
            yield return null;
        }

        cardCanvasGroup.alpha = 1f;  // ���������� �����ϰ� ���̰� ����

        // ī�� ������ ȿ�� �߰� (ȸ�� ȿ��)
        StartCoroutine(CardFlipEffect());

        // ������ ��ư Ȱ��ȭ
        ExitBtn.gameObject.SetActive(true);
    }

    // ī�尡 ���������� ȿ��
    private IEnumerator CardFlipEffect()
    {
        float rotationAmount = 0f;
        Quaternion startRotation = card.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 180, 0); // 180�� ȸ��

        // ī�尡 õõ�� ȸ���ϵ��� ó��
        while (rotationAmount < 1f)
        {
            rotationAmount += Time.deltaTime / fadeDuration;
            card.transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationAmount);
            yield return null;
        }

        card.transform.rotation = endRotation; // ���������� ���� ȸ��
    }

    // ������ ��ư Ŭ�� �� ī��� Fade Black ��Ȱ��ȭ
    public void HideCardAndFadeBlack()
    {
        GachaFadeBlack.gameObject.SetActive(false); // Fade Black ��Ȱ��ȭ
        card.SetActive(false); // ī�� ��Ȱ��ȭ

        // �̱� ��ư Ȱ��ȭ
        DrawOneBtn.interactable = true;
        DrawTenBtn.interactable = true;

        // Exit ��ư ��Ȱ��ȭ
        ExitBtn.gameObject.SetActive(false);
    }

    // ī�� ȸ�� ���� ����
    private void ResetCardRotation()
    {
        card.transform.rotation = Quaternion.Euler(0, 0, 0); // ī�� ȸ�� �ʱ�ȭ
    }

    // �̱� ��ư�� �ٽ� ������ �� ī��� ���̵� ���� �ʱ�ȭ�Ͽ� ���� ��µǰ� ����
    public void ResetForNewDraw()
    {
        HideCardAndFadeBlack(); // ���� ī��� Fade Black ��Ȱ��ȭ

        // ī�尡 ���� ���� �� �ٽ� Ȱ��ȭ�ǰ� �ʱ� ���·� ������
        DrawOneBtn.interactable = false; // �̱� ��ư ��Ȱ��ȭ
        DrawTenBtn.interactable = false;

        // ī�� ȸ�� ���� �ʱ�ȭ
        ResetCardRotation(); // ī�� ȸ�� ����
        card.SetActive(true); // ī�� Ȱ��ȭ

        // ���ο� �̱⸦ ����
        StartCoroutine(FadeInEffect(1));  // ���� �̱� ����
    }
}
