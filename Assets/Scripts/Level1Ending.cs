using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Ending : MonoBehaviour
{
    // Image
    public Image image;  // Image를 public으로 선언
    public Sprite[] sprites;  // public으로 sprite 배열 선언
    int imageIndex = 0; // sprite 배열의 순서, 첫 번째는 0부터 시작

    // Text
    public TMP_Text endingText;
    string[] dialogues; // 대사 배열로 선언
    int dialogueIndex = 0; // 현재 대사 인덱스
    bool isTyping = false; // 대사 출력 중인지 확인하는 플래그

    // 터치 감지 및 안내 텍스트
    public TMP_Text touchToScreenText; // "Touch to Screen!" 텍스트
    private float touchTimer = 0f; // 터치 타이머
    private const float touchThreshold = 5f; // 터치 감지 시간

    void Start()
    {
        // 대사 배열 초기화
        dialogues = new string[]
        {
            "이번주 핫이슈는 햄버거 가게의 폐업 소식 입니다 !",
            "얼마전 신장 개업한 ‘좋은 햄버거 맛있는 햄버거’ 가게 다시 폐업한 소식입니다.",
            "‘햄버거 업고 튀어’ 가게보다 매출률이 3배나 밀리는 현상을 보였습니다.",
            "이로 인해 ‘좋은 햄버거 맛있는 햄버거’ 가게에선 폐업을 결정한 것으로 보입니다.",
            "‘좋은 햄버거 맛있는 햄버거’ 가게의 다음 행보를 기대하며 다음 뉴스 입니다 !"
        };

        // Image 컴포넌트가 할당되지 않은 경우 GetComponent를 통해 가져옴
        if (image == null)
        {
            image = GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component is not assigned and could not be found on the GameObject.");
                return;
            }
        }

        // 첫 번째 대사 자동 출력
        StartCoroutine(ShowDialogue());

        // "Touch to Screen!" 텍스트 비활성화
        if (touchToScreenText != null)
        {
            touchToScreenText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 화면 터치 입력을 감지
        if (Input.GetMouseButtonDown(0) && !isTyping && dialogueIndex < dialogues.Length)
        {
            touchTimer = 0f; // 터치가 감지되면 타이머를 초기화
            if (touchToScreenText != null)
            {
                touchToScreenText.gameObject.SetActive(false); // 텍스트 비활성화
            }
            StartCoroutine(ShowDialogue());
        }
        else
        {
            touchTimer += Time.deltaTime; // 터치가 없으면 타이머 증가
            if (touchTimer >= touchThreshold)
            {
                if (touchToScreenText != null)
                {
                    touchToScreenText.gameObject.SetActive(true); // 5초 이상 터치가 없으면 텍스트 활성화
                }
            }
        }
    }

    // 텍스트 타이핑 효과
    IEnumerator Typing(string t)
    {
        // 대사 출력 중으로 설정
        isTyping = true;
        // text를 null 값으로 설정
        endingText.text = null;
        for (int i = 0; i < t.Length; i++)
        {
            endingText.text += t[i];
            // 속도
            yield return new WaitForSeconds(0.05f);
        }
        // 대사 출력 완료
        isTyping = false;
    }

    // 대사 출력 코루틴
    IEnumerator ShowDialogue()
    {
        // 현재 대사 출력
        yield return StartCoroutine(Typing(dialogues[dialogueIndex]));

        // 대사 인덱스가 1부터이면 이미지 변경
        if (dialogueIndex > 0 && imageIndex < sprites.Length)
        {
            image.sprite = sprites[imageIndex];
            imageIndex++;
        }

        // 대사 인덱스 증가
        dialogueIndex++;

        // 마지막 대사가 끝난 후 MainScene으로 이동
        if (dialogueIndex >= dialogues.Length)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndingScene");
        }
    }
}
