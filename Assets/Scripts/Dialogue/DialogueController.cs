using UnityEngine;
using UnityEngine.UI;

/*
 * Used to manage the dialogue and dialogue options on screen for the player to select.
 */
public class DialogueController : MonoBehaviour
{
    public Canvas canvas;
    public Text[] dialogueOptionText;
    public Text dialogueText;

    private DialogueOptionHolder dialogueOptionHolder;
    private DialogueHolder dialogueHolder;
    private Dialogue dialogue;
    private DialogueOption[] dialogueOptions;
    private DialogueOptionTextHelper[] dialogueOptionTextHelpers;
    private int currentSelection;
    private int activeDialogueOptionCount;

    public void Awake()
    {
        canvas.enabled = false;
        dialogueOptions = new DialogueOption[dialogueOptionText.Length];
        dialogueOptionTextHelpers = new DialogueOptionTextHelper[dialogueOptionText.Length];
        for(int i = 0; i < dialogueOptionText.Length; i++)
        {
            DialogueOptionTextHelper textHelper = new DialogueOptionTextHelper(dialogueOptionText[i], i+1);
            dialogueOptionTextHelpers[i] = textHelper;
        }

        currentSelection = 0;
        activeDialogueOptionCount = 0;

        ActionController actionController = Singleton<ActionController>.Instance;
        actionController.registerStartButtonListener(InputButton.DIALOGUE_UP, dialogueSelectionMove);
        actionController.registerStartButtonListener(InputButton.DIALOGUE_DOWN, dialogueSelectionMove);

        actionController.registerStartButtonListener(InputButton.DIALOGUE_SELECT, dialogueOptionSelect);

        actionController.registerStartButtonListener(InputButton.DIALOGUE_SELECT, dialogueOptionSelect);
        actionController.registerStartButtonListener(InputButton.DIALOGUE_SELECT, dialogueOptionSelect);
        actionController.registerStartButtonListener(InputButton.DIALOGUE_SELECT, dialogueOptionSelect);
        actionController.registerStartButtonListener(InputButton.DIALOGUE_SELECT, dialogueOptionSelect);
    }

    public void enableUI(bool enable)
    {
        canvas.enabled = enable;
    }

    public void setDialogueHolder(DialogueHolder dialogueHolder)
    {
        this.dialogueHolder = dialogueHolder;
    }

    public void setDialogueOptionHolder(DialogueOptionHolder dialogueOptionHolder)
    {
        this.dialogueOptionHolder = dialogueOptionHolder;
    }

    public void setDialogue(string dialogueId)
    {
        dialogue = dialogueHolder.getDialog(dialogueId);
        dialogueText.text = dialogue.text;
        setDialogueOptions(dialogue.dialogueOptionIds);
    }

    private void setDialogueOptions(string[] dialogueOptionIds)
    {
        currentSelection = 0;
        int count = 0;
        foreach(string dialogueOptionId in dialogueOptionIds)
        {
            if (count > dialogueOptionText.Length-1)
                break;
            Debug.Log("COUNT " + count);

            DialogueOption dialogueOption = dialogueOptionHolder.getDialogOption(dialogueOptionId);
            dialogueOptions[count] = dialogueOption;

            DialogueOptionTextHelper textHelper = dialogueOptionTextHelpers[count];
            textHelper.updateText(dialogueOption.text);
            textHelper.deselected();
            textHelper.enableText(true);
            count++;
        }
        activeDialogueOptionCount = count - 1;

        for(int i = count; i<dialogueOptionTextHelpers.Length; i++)
        {
            dialogueOptionTextHelpers[i].enableText(false);
        }

        dialogueOptionTextHelpers[0].selected();
    }

    public void dialogueSelectionMove(InputButton button)
    {
        if (button == InputButton.DIALOGUE_UP)
        {
            dialogueOptionTextHelpers[currentSelection].deselected();
            currentSelection--;
        }
        else if (button == InputButton.DIALOGUE_DOWN)
        {
            dialogueOptionTextHelpers[currentSelection].deselected();
            currentSelection++;
        }
        else
        {
            return;
        }

        if (currentSelection < 0)
            currentSelection = activeDialogueOptionCount;
        else if (currentSelection > activeDialogueOptionCount)
            currentSelection = 0;

        dialogueOptionTextHelpers[currentSelection].selected();
    }

    public void specificDialogueOptionSelected(InputButton button)
    {
        int previousSelection = currentSelection;

        if (button == InputButton.DIALOGUE_ONE)
            currentSelection = 0;
        else if (button == InputButton.DIALOGUE_TWO)
            currentSelection = 1;
        else if (button == InputButton.DIALOGUE_THREE)
            currentSelection = 2;
        else if (button == InputButton.DIALOGUE_FOUR)
            currentSelection = 3;
        else
            return;

        if (currentSelection > activeDialogueOptionCount)
        {
            currentSelection = previousSelection;
            return;
        }

        dialogueOptionTextHelpers[previousSelection].deselected();
        dialogueOptionTextHelpers[currentSelection].selected();

        dialogueOptionSelect(InputButton.DIALOGUE_SELECT);
    }

    public void dialogueOptionSelect(InputButton button)
    {
        if (button != InputButton.DIALOGUE_SELECT)
            return;

        /*
         * TODO figure out some kind of transition. Two I can think of off the top of my head
         * 1) Flicker the selected and un-selected colors for a small amount of time.
         * 2) Fade out the options not selected first, leaving the selected option. Then fade out the selection.
         */
        Debug.Log("DIALOGUE SELECTED!!! " + dialogueOptions[currentSelection].text);
    }
}