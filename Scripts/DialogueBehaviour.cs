using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogueBehaviour : MonoBehaviour
{
    float signTimer;
    int signIndex;
    protected bool writin;
    protected bool forceFill;

    protected void FixButtons(GameObject[] list, DialogueNode[] nodes)
    {
        int enabledButtons = nodes.Length;
        for (int i = 0; i < list.Length; i++)
        {
            try
            {
                if (i < enabledButtons && (nodes[i].requiredEvidence == "" ||
                    JournalText.HaveEvidence(nodes[i].requiredEvidence)))
                {
                    list[i].SetActive(true);
                    list[i].GetComponentInChildren<Text>().text = i + 1 + ". " + nodes[i].nodeTitle;
                }
                else
                {
                    list[i].SetActive(false);
                }
            }
            catch
            {
                list[i].SetActive(false);
            }
        }
    }

    protected void FixButtons(GameObject[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].SetActive(false);
        }
    }

    protected void UpdateTextBoard(Text board, DialogueNode node, int index, float delay)
    {
        string targetString = "NULL";
        targetString = node.npcText[index];

        if (board.text.Length < targetString.Length)
        {
            if (!forceFill)
            {
                if (signTimer <= 0)
                {
                    board.text += targetString[signIndex];
                    signIndex++;
                    signTimer = delay;
                }
            }
            else
            {
                board.text = targetString;
                forceFill = false;
            }
            writin = true;
        }
        else
            writin = false;

        //board.text = targetString;

        signTimer -= Time.deltaTime;
    }

    protected void ClearBoard(Text board)
    {
        board.text = "";
        signIndex = 0;
    }

    protected void AddBullshit()
    {

    }
}
