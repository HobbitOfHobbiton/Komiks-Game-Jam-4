using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    public string nodeTitle = "Wolololo";
    public string[] npcText;
    public bool[] isImportant;
    public bool[] isLie;

    public string requiredEvidence;
    public string evidence;
    public Character.CharacterType evidenceAgainst;

    public DialogueNode[] avaibleNodes;
    public DialogueNode bullshitNode;


    public enum NodeType
    {
        Standard,
        Quiter
    }
    public NodeType nodeType = NodeType.Standard;

    public void GetEvidence()
    {
        if (evidence != null)
        {
            for (int i = 0; i < Character.listOfCharacters.Count; i++)
            {
                if (Character.listOfCharacters[i].character == evidenceAgainst
                    && evidence != "")
                {
                    Character.listOfCharacters[i].AddEvidence(evidence);
                }
            }
        }
    }

    public void CheckBullshit()
    {
        if(bullshitNode != null)
        {
            //Debug.LogWarning("BullshitDetected");
        }
    }

}
