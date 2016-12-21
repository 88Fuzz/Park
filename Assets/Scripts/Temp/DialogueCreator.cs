using UnityEngine;

public class DialogueCreator : MonoBehaviour
{
    public string fileName;
    public DialogueController dialogueController;

    public void Awake()
    {
        string[] dialogueOptionIds = getDialogueOptionIds(4);

        DialogueHolder dialogueHolder = new DialogueHolder();
        DialogueOptionHolder dialogueOptionHolder = new DialogueOptionHolder();

        Dialogue rootDialogue = createDialogue("This is a test you butt!", dialogueOptionIds);
        Dialogue lastDialogue = createDialogue("I new dialogue message you say? Well it worked!", null);
        dialogueHolder.addDialogue(rootDialogue);
        dialogueHolder.addDialogue(lastDialogue);

        dialogueOptionHolder.addDialogueOption(createDialogueOption("Yes!", dialogueOptionIds[0], lastDialogue.id));
        dialogueOptionHolder.addDialogueOption(createDialogueOption("No!", dialogueOptionIds[1], lastDialogue.id));
        dialogueOptionHolder.addDialogueOption(createDialogueOption("Uh, fuck you!", dialogueOptionIds[2], lastDialogue.id));
        dialogueOptionHolder.addDialogueOption(createDialogueOption("...", dialogueOptionIds[3], lastDialogue.id));

        FileUtils.writeFile(fileName + ".DialogueHolder.json",dialogueHolder.toJsonString());
        FileUtils.writeFile(fileName + ".DialogueOptionHolder.json",dialogueOptionHolder.toJsonString());


        dialogueController.setDialogueHolder(dialogueHolder);
        dialogueController.setDialogueOptionHolder(dialogueOptionHolder);
        dialogueController.setDialogue(rootDialogue.id);
        dialogueController.enableUI(true);
    }

    private string[] getDialogueOptionIds(int count)
    {
        string[] dialogueOptionIds = new string[count];

        for(int i = 0; i < count; i++)
        {
            dialogueOptionIds[i] = System.Guid.NewGuid().ToString();
        }

        return dialogueOptionIds;
    }

    private Dialogue createDialogue(string text, string[] dialogOptionIds)
    {
        Dialogue dialog = new Dialogue();
        dialog.id = System.Guid.NewGuid().ToString();
        dialog.text = text;
        dialog.dialogueOptionIds = dialogOptionIds;

        return dialog;
    }

    private DialogueOption createDialogueOption(string text, string id, string nextDialogueId)
    {
        DialogueOption dialogueOption = new DialogueOption();
        dialogueOption.id = id;
        dialogueOption.text = text;
        dialogueOption.nextDialogId = nextDialogueId;

        return dialogueOption;
    }
}