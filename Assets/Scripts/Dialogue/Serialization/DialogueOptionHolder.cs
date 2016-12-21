using UnityEngine;
using System.Collections.Generic;

/*
 * Wrapper class around a Dictionary<DialogueOptionId (String), DialogueOption>. The class exposes methods to get Dialogue options based on the dialogueOptionId, 
 * serialize the dictionary, un-serialize the dictionary, and creating the dictionary for testing purposes.
 */
public class DialogueOptionHolder
{
    private IDictionary<string, DialogueOption> dialogueOptionMap;

    public static DialogueOptionHolder createFromJsonString(string json)
    {
        SerializableArrayWrapper<DialogueOption> wrapper = JsonUtility.FromJson<SerializableArrayWrapper<DialogueOption>>(json);
        DialogueOptionHolder dialogueOptionHolder = new DialogueOptionHolder();
        foreach(DialogueOption dialogueOption in wrapper.items)
        {
            dialogueOptionHolder.addDialogueOption(dialogueOption);
        }

        return dialogueOptionHolder;
    }

    public DialogueOptionHolder()
    {
        dialogueOptionMap = new Dictionary<string, DialogueOption>();
    }

    public DialogueOption getDialogOption(string dialogueOptionId)
    {
        return dialogueOptionMap[dialogueOptionId];
    }

    public string toJsonString()
    {
        DialogueOption[] dialogueArray = new DialogueOption[dialogueOptionMap.Count];
        int count = 0;
        foreach(DialogueOption dialogueOption in dialogueOptionMap.Values)
        {
            dialogueArray[count++] = dialogueOption;
        }

        SerializableArrayWrapper<DialogueOption> wrapper = new SerializableArrayWrapper<DialogueOption>();
        wrapper.items = dialogueArray;
        return JsonUtility.ToJson(wrapper);
    }

    public void addDialogueOption(DialogueOption dialogueOption)
    {
        dialogueOptionMap[dialogueOption.id] = dialogueOption;
    }
}