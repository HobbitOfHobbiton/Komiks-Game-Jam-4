using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : DialogueBehaviour
{
    public GameObject buttonPrefab;
    public RectTransform startSpawnPoint;
    public GameObject currentTextBackground;
    public Text textBoard;
    public float typingDelay = 0.1f;

    public GameObject[] buttons;
    public static DialogueNode currentNode;

    bool canWork;
    public static int textIndex;

    public static DialogueSystem dialog;
    public static bool isTalking = false;

    private void Awake()
    {
        dialog = this;
        isTalking = false;
        FixButtons(buttons);
    }

    void Start()
    {
        StartDialog();
    }

    public void StartDialog()
    {
        canWork = NotErrorsFound();

        if (canWork)
        {
            FixButtons(buttons, currentNode.avaibleNodes);
            ClearBoard(textBoard);
            startSpawnPoint.gameObject.SetActive(false);
            RoomManager.inConversation = true;
        }
    }

    public void NextDialog()
    {
        shouldMoveOn = true;
    }
    bool shouldMoveOn = false;

    void Update()
    {
        if (canWork)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !JournalText.JorunalActive())
            {
                if (!writin && textIndex < currentNode.npcText.Length)
                    textIndex++;
                else
                    forceFill = true;
                
                if (textIndex < currentNode.npcText.Length)
                    ClearBoard(textBoard);
            }

            if (textIndex < currentNode.npcText.Length)
            {
                startSpawnPoint.gameObject.SetActive(false);
                UpdateTextBoard(textBoard, currentNode, textIndex, typingDelay);

                //todo 
                Character character = Character.GetCharacter(0);
                for (int i = 0; i < currentNode.npcText.Length; i++)
                {
                    character.AddDialog(currentNode.npcText[i], currentNode.isImportant[i], true, currentNode.isLie[i]);

                    if (currentNode.isLie[i]) JournalText.allLies.Add(currentNode.npcText[i]);

                }
                currentNode.GetEvidence();
            }
            else
            {
                startSpawnPoint.gameObject.SetActive(true);
            }

            FixButtons(buttons, currentNode.avaibleNodes);
        }
    }

    bool NotErrorsFound()
    {
        if (buttonPrefab == null || startSpawnPoint == null || currentNode == null || textBoard == null)
        {
            //Debug.LogError("Czegoś nie przypisałeś w systemie dialogów panocku");
            return false;
        }

        return true;
    }

    public void SelectButton(int index)
    {
        DialogueNode temp = currentNode.avaibleNodes[index];
        currentNode = temp;
        textIndex = 0;
        ClearBoard(textBoard);

        if (currentNode.nodeType == DialogueNode.NodeType.Quiter)
        {
            isTalking = false;
            RoomManager.inConversation = false;
            if (currentTextBackground)
                currentTextBackground.SetActive(false);
        }
    }

    public void Mindreader()
    {
        if (isTalking)
        {
            try
            {
                currentNode.CheckBullshit();

                if (currentNode.isLie[textIndex-1])
                {
                    Debug.Log("Lie detected");
                    if (currentNode.bullshitNode != null)
                    {

                    }
                }
                else
                {
                    Debug.Log("Miss");
                }
            }
            catch
            {
                Debug.Log("Catch");
            }
        }
        else
            Debug.LogError("U faggot, u can't use bullshit detector right now");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 100, 100), textIndex.ToString());
    }
}
