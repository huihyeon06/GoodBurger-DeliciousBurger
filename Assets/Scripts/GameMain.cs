using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    // Image
    public Image characterImage; // 캐릭터 이미지
    public Image orderImage; // 음식 주문 이미지

    // Text
    public Text orderText;  // UI Text 요소를 연결하기 위한 변수
    public Text levelText;  // 레벨을 표시할 UI Text 요소
    public Text orderMessageText; // 주문 메세지 텍스트

    // Button
    public Button yesBtn; // 주문 시 '네' 버튼
    public Button noBtn; // 주문 시 '아니요' 버튼

    // 변수 선언
    private int currentOrder = 1; // 주문수
    private int totalOrder = 8; // 최대 주문
    private int currentLevel = 1; // 현재 레벨

    // 랜덤 주문 메시지 배열
    private string[] messages = { "띠드버거 주세욤 !!",
        "새우가 드라마를 찍으면? 대하드라마 !! " + "하하 !! 새우버거 하나 주세요 !",
        "아주 매운 햄버거 주세요 !",
        "패티가 따블 !! 더블패티 하나요 !"};

    void Start()
    {
        characterImage.gameObject.SetActive(false); // 캐릭터 이미지 비활성화
        orderImage.gameObject.SetActive(false); // 음식 주문 이미지 비활성화
        yesBtn.gameObject.SetActive(false); // '네' 버튼 비활성화
        noBtn.gameObject.SetActive(false); // '아니요' 버튼 비활성화
        orderMessageText.gameObject.SetActive(false); // 주문 메세지 텍스트 비활성화

        /* UpdateOrderText();
                UpdateLevelText(); */

        // '아니요' 버튼 누를 시 다시 주문 할 수 있는 코루틴
        StartCoroutine(ShowCharacterImageAfterDelay(2.5f));

        yesBtn.onClick.AddListener(OnYesButtonClick);
        noBtn.onClick.AddListener(OnNoButtonClick);
    }

    void Update()
    {

    }

    // 주문 수 증가 (text)
    private void UpdateOrderText()
    {
        if (orderText != null)
        {
            orderText.text = currentOrder + "/" + totalOrder;
        }
        else
        {
            Debug.LogError("Order Text is not assigned!");
        }
    }

    // 레벨 증가 (text)
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Lv " + currentLevel;
        }
        else
        {
            Debug.LogError("Level Text is not assigned!");
        }
    }

    // 고객 이미지 n초 후 나타남
    private IEnumerator ShowCharacterImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterImage != null)
        {
            characterImage.gameObject.SetActive(true); // 캐릭터 이미지를 활성화
            orderImage.gameObject.SetActive(true); // 음식 주문 이미지 활성화
            yesBtn.gameObject.SetActive(true); // '네' 버튼 활성화
            noBtn.gameObject.SetActive(true); // '아니요' 버튼 활성화
            orderMessageText.gameObject.SetActive(true); // 주문 메시지 텍스트 활성화
            UpdateOrderMessageText(); // 랜덤 메시지 설정
        }
        else
        {
            Debug.LogError("Character Image is not assigned!");
        }
    }

    // 주문 시 '네' 버튼 
    void OnYesButtonClick()
    {
        if (currentOrder < totalOrder)
        {
            currentOrder++;
        }
        else
        {
            currentOrder = 1;
            currentLevel++;
            UpdateLevelText();
        }
        UpdateOrderText();

        // '네' 버튼 누를 시 주문대로 이동
        SceneManager.LoadScene("MainGameScene");
    }

    // 주문 시 '아니요' 버튼
    void OnNoButtonClick()
    {
        StartCoroutine(HideAndShowImagesAfterDelay(2.5f));
    }

    // '아니요' 버튼 누를 시
    private IEnumerator HideAndShowImagesAfterDelay(float delay)
    {
        // 이미지와 버튼들을 비활성화
        characterImage.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        orderMessageText.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        // 이미지와 버튼들을 다시 활성화
        characterImage.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
        orderMessageText.gameObject.SetActive(true);
        UpdateOrderMessageText(); // 랜덤 메시지 설정
    }

    // 랜덤으로 주문 메시지 설정
    private void UpdateOrderMessageText()
    {
        if (orderMessageText != null)
        {
            orderMessageText.text = messages[Random.Range(0, messages.Length)];
        }
        else
        {
            Debug.LogError("Order Message Text is not assigned!");
        }
    }
}
