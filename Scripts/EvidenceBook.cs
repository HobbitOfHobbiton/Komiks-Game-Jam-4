using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceBook : MonoBehaviour
{
    public static EvidenceBook Evidence { get; private set; }

    public Text nameLabel;
    public GameObject portraitsButtons;
    public GameObject nextButton, backButton;
    public Text[] evidenceToDisplay;
    public int currentPageIndex = 0;

    private Character _currentCharacter;

    private void Awake()
    {
        Evidence = this;
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
        if (evidenceToDisplay == null) return;
        for (int i = 0; i < evidenceToDisplay.Length; i++)
        {
            if (_currentCharacter.characterDialogs == null) return;
            if (i > _currentCharacter.characterDialogs.Count) return;
            ShowEvidence();
        }
    }

    public void NextPage()
    {
        if (_currentCharacter == null) _currentCharacter = Character.listOfCharacters[0];

        if (_currentCharacter.characterDialogs.Count <= currentPageIndex * evidenceToDisplay.Length) return;
        currentPageIndex++;
        CheckButton();
        ShowEvidence();
    }

    public void PreviousPage()
    {
        if (currentPageIndex == 0) return;
        currentPageIndex--;
        CheckButton();
        ShowEvidence();
    }

    private void CheckButton()
    {
        try
        {
            backButton.SetActive(currentPageIndex == 0 ? false : true);
            nextButton.SetActive(_currentCharacter.characterDialogs.Count <= currentPageIndex * evidenceToDisplay.Length ? false : true);
        }
        catch
        {

        }
    }

    private void ShowEvidence()
    {
        for (int i = 0; i < evidenceToDisplay.Length; i++)
        {
            int index = i + currentPageIndex * evidenceToDisplay.Length;

            if (index >= _currentCharacter.evidenceAgainst.Count)
            {
                evidenceToDisplay[i].text = "";
            }
            else
            {
                evidenceToDisplay[i].text = _currentCharacter.evidenceAgainst[index];
            }
        }
    }

    public static bool BookActive()
    {
        return Evidence.transform.parent.gameObject.activeInHierarchy;
    }


}
