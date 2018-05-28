using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static List<Character> listOfCharacters = new List<Character>();

    [SerializeField]
    public List<Dialogs> characterDialogs = new List<Dialogs>();
    public List<string> evidenceAgainst = new List<string>();
    public Sprite portrait;
    public CharacterType character;
    public string characterName;

    public enum CharacterType
    {
        Regina,
        Marco,
        Bartholomeo,
        Mirelle,
        Rebecca,
        Dorian,
        Cedric,
        Donnaa
    }

    private void Awake()
    {
        if (character != CharacterType.Cedric)
            listOfCharacters.Add(this);
    }

    public void AddDialog(string dialog, bool writeInJorunal, bool skillUsed, bool wasTrue)
    {
        bool? isDialogTrue = null;
        if (skillUsed)
            isDialogTrue = wasTrue;
        for (int i = 0; i < characterDialogs.Count; i++)
        {
            if (characterDialogs[i].dialog == dialog)
            {
                characterDialogs[i] = new Dialogs(dialog, isDialogTrue, 0);
                return;
            }
        }
        Dialogs newDialog = new Dialogs(dialog, isDialogTrue, 0);
        characterDialogs.Add(newDialog);
    }

    public void AddEvidence(string evidence)
    {
        for (int i = 0; i < evidenceAgainst.Count; i++)
        {
            if (evidenceAgainst[i] == evidence) return;
        }
        evidenceAgainst.Add(evidence);
        JournalText.AddEvidence(evidence);
    }

    public static Character GetCharacter(string name)
    {
        for (int i = 0; i < listOfCharacters.Count; i++)
        {
            if (listOfCharacters[i].characterName == name)
                return listOfCharacters[i];
        }
        Debug.LogError("No character " + name);
        return null;
    }

    public static Character GetCharacter(CharacterType character)
    {
        for (int i = 0; i < listOfCharacters.Count; i++)
        {
            if (listOfCharacters[i].character == character)
                return listOfCharacters[i];
        }
        Debug.LogError("No character " + character.ToString() + "found");
        return null;
    }

    public static Character GetCharacter(int index)
    {
        if (index < 0 || index > listOfCharacters.Count) return null;
        return listOfCharacters[index];
    }

    public void UpdateMarkerIndex(int dialogIndex, int markerIndex)
    {
        characterDialogs[dialogIndex].markerIndex = markerIndex;
    }
}

[System.Serializable]
public class Dialogs
{
    public Dialogs(string dialog, bool? wasTrue, int playerMarker)
    {
        this.dialog = dialog;
        this.wasTrue = wasTrue;
        this.markerIndex = playerMarker;
    }

    public string dialog = "";
    public bool? wasTrue;
    public int markerIndex;
}