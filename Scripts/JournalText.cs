using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalText : MonoBehaviour
{
    public static JournalText Journal { get; private set; }

    public Text nameLabel;
    public GameObject portraitsButtons;
    public GameObject nextButton, backButton;
    public DialogLabel[] dialogToDisplay;
    public int currentPageIndex = 0;

    private Character _currentCharacter;

    private void Awake()
    {
        Journal = this;
    }

    private void Start()
    {
        for (int i = 0; i < portraitsButtons.transform.childCount; i++)
        {
            Image portrait = portraitsButtons.transform.GetChild(i).GetComponent<Button>().image;
            portrait.sprite = Character.GetCharacter(i).portrait;
        }
        //transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (DialogueSystem.isTalking)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        loadCharacter(0);
        CheckButton();
    }

    public void loadCharacter(int index)
    {
        if (index < 0 || index >= Character.listOfCharacters.Count)
            return;
        _currentCharacter = Character.listOfCharacters[index];
        if (nameLabel) nameLabel.text = _currentCharacter.characterName;
        currentPageIndex = 0;
        if (dialogToDisplay == null) return;
        for (int i = 0; i < dialogToDisplay.Length; i++)
        {
            if (_currentCharacter.characterDialogs == null) return;
            if (i > _currentCharacter.characterDialogs.Count) return;
            ShowDialog();
        }
    }

    public void NextPage()
    {
        if (_currentCharacter == null) _currentCharacter = Character.listOfCharacters[0];

        if (_currentCharacter.characterDialogs.Count <= currentPageIndex * dialogToDisplay.Length) return;
        currentPageIndex++;
        CheckButton();
        ShowDialog();
    }

    public void PreviousPage()
    {
        if (currentPageIndex == 0) return;
        currentPageIndex--;
        CheckButton();
        ShowDialog();
    }

    private void CheckButton()
    {
        try
        {
            backButton.SetActive(currentPageIndex == 0 ? false : true);
            nextButton.SetActive(_currentCharacter.characterDialogs.Count <= currentPageIndex * dialogToDisplay.Length ? false : true);
        }
        catch
        {

        }
    }

    private void ShowDialog()
    {
        for (int i = 0; i < dialogToDisplay.Length; i++)
        {
            int index = i + currentPageIndex * dialogToDisplay.Length;
            if (_currentCharacter == null || index >= _currentCharacter.characterDialogs.Count)
            {
                Activate(i, false);
            }
            else
            {
                dialogToDisplay[i].text.text = _currentCharacter.characterDialogs[index].dialog;
                dialogToDisplay[i].trueToggles.UpdateToggles(_currentCharacter.characterDialogs[index].wasTrue);
                dialogToDisplay[i].playerMarker.UpdateToogles(_currentCharacter.characterDialogs[index].markerIndex); 
                Activate(i, true);
            }
        }
    }

    private void Activate(int index, bool activate)
    {
        dialogToDisplay[index].transform.GetChild(0).gameObject.SetActive(activate);
        dialogToDisplay[index].transform.GetChild(1).gameObject.SetActive(false);

        //dialogToDisplay[index].transform.GetChild(1).gameObject.SetActive(activate);
    }

    public void UpdateTogggleMarker(int dialogIndex, int markerIndex)
    {
        int index = currentPageIndex * 8 + dialogIndex;
        if (index > _currentCharacter.characterDialogs.Count) return;
        _currentCharacter.UpdateMarkerIndex(index, markerIndex);
    }

    public static bool JorunalActive()
    {
        if (!Journal) return false;
        return Journal.transform.parent.gameObject.activeInHierarchy;
    }

    public static List<string> allLies = new List<string>();

    public static void AddEvidence(string evidence)
    {
        for (int i = 0; i < allLies.Count; i++)
        {
            if (allLies[i] == evidence)
                return;
        }
        allLies.Add(evidence);
    }

    public static bool HaveEvidence(string evidence)
    {
        for (int i = 0; i < allLies.Count; i++)
        {
            if (allLies[i] == evidence) return true;
        }
        return false;
    }
}
