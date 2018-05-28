using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterToTalk : MonoBehaviour, IPointerDownHandler
{
    public Character.CharacterType character;
    public DialogueNode characterNode;

    public GameObject textBackground;
    public Text text;

    public static Character lastCharacterPlayerTalk;

    private void Start()
    {
        if (textBackground) textBackground.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (DialogueSystem.isTalking) return;
        DialogueSystem.currentNode = characterNode;
        DialogueSystem.isTalking = true;

        DialogueSystem.textIndex = 0;

        textBackground.SetActive(true);
        DialogueSystem.dialog.textBoard = text;
        DialogueSystem.dialog.currentTextBackground = textBackground;

        DialogueSystem.dialog.StartDialog();

        lastCharacterPlayerTalk = Character.GetCharacter(character);
    }
}
