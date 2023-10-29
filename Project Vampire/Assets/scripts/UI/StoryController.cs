using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class OpeningStory : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public GameObject mainMenu;
    public TextMeshProUGUI continueText;
    public Image background;
    private int currentPosition = 0;
    public KeyCode continueKey = KeyCode.Space;

    private bool isScrolling = true;
    private bool showContinuePrompt = false;

    private RectTransform storyTextRectTransform;

    void Start()
    {
        mainMenu.SetActive(false);
        continueText.gameObject.SetActive(false);
        storyTextRectTransform = storyText.rectTransform;
        background.color = Color.black;
        StartCoroutine(ScrollText());
    }

    void Update()
    {
        if (isScrolling && Input.GetKeyDown(continueKey))
        {
            StopAllCoroutines();
            storyText.text = "";
            showContinuePrompt = true;
        }

        if (showContinuePrompt && Input.GetKeyDown(continueKey))
        {
            ShowMainMenu();
        }
    }

    IEnumerator ScrollText()
    {
        string story = "As a powerful Vampire Lord, you've spent centuries in peaceful seclusion, residing in a castle deep within the Black Forest of the Kingdom of Gurande. This peace was abruptly shattered with the ascension of the current monarch, the avaricious and foolish King Lance IV. This king brazenly sent his impertinent envoy into your sanctuary, declaring that you must either submit to his reign or meet your end. Enraged by the audacity, you beheaded the envoy and sent his round, fat head back to the king as your unequivocal response.\nInfuriated and humiliated, King Lance IV has now rallied his armies and called upon mercenaries and adventurers from both within his kingdom and from foreign lands to exterminate you, the \"evil\" vampire. The time has come to marshal your undead legions, beings of ancient and noble blood. Perhaps a lesson steeped in the harsh reality of mortality is what this naive king desperately needs.";


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
