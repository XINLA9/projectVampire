using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingStory : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public GameObject winPage;
    public GameObject mainMenu;
    public TextMeshProUGUI continueText;
    public Image background;
    private int currentPosition = 0;
    public KeyCode continueKey = KeyCode.Space;

    private bool isScrolling = true;
    private bool showContinuePrompt = false;
    private bool isEnding = false;

    private RectTransform storyTextRectTransform;

    private void OnEnable() {
        mainMenu.SetActive(false);
        continueText.gameObject.SetActive(false);
        storyTextRectTransform = storyText.rectTransform;
        background.color = Color.black;
        isEnding = true;
        winPage.SetActive(true);
        StartCoroutine(ScrollText());
    }

    void Update()
    {
        if (isEnding){
            if (isScrolling && Input.GetKeyDown(continueKey))
            {
                StopAllCoroutines();
                storyText.text = "";
                showContinuePrompt = true;
            }

            if (showContinuePrompt && Input.GetKeyDown(continueKey))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator ScrollText()
    {
        string story = "The war has ended, and in the silence of the Black Forest, only the wind speaks. The horns of King Lance IV have fallen silent, his armies and mercenaries defeated at your feet. The creatures of the forest recognize their true ruler—you, the Vampire Lord of Gurande.\nLance's ambition led to his downfall; with mercy, you ended his reign. With the tyrant's fall, an era of oppression has ended. The people regard you with awe and gratitude, not as a monster, but as a guardian.\nReturning to your castle, your sanctuary is at peace once more. The lesson you've imparted will endure, and peace in the Black Forest will be preserved under your watch. In the predawn darkness, you affirm that this ancient land will forever remain your realm, where legend and reality are woven under your reign.";
        while (currentPosition < story.Length)
        {
            storyText.text += story[currentPosition];
            currentPosition++;
            storyTextRectTransform.anchoredPosition = new Vector2(storyTextRectTransform.anchoredPosition.x, storyTextRectTransform.anchoredPosition.y + 1);
            background.color = Color.Lerp(background.color, Color.black, Time.deltaTime * 2);

            yield return new WaitForSeconds(0.02f);
        }

        showContinuePrompt = true;
        continueText.gameObject.SetActive(true);
    }

    void ShowMainMenu()
    {
        isScrolling = false;
        mainMenu.SetActive(true);
        continueText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        storyText.gameObject.SetActive(false);
    }
}
