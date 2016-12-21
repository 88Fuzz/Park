using UnityEngine;
using System.Collections.Generic;

/*
 * Wrapper class around a Dictionary<DialogueId (String), Dialogue>. The class exposes methods to get Dialogue options based on the dialogueId,
 * serialize the dictionary, un-serialize the dictionary, and creating the dictionary for testing purposes.
 */
public class DialogueHolder
{
    private IDictionary<string, Dialogue> dialogueMap;

    public static DialogueHolder createFromJsonString(string json)
    {
        SerializableArrayWrapper<Dialogue> wrapper = JsonUtility.FromJson<SerializableArrayWrapper<Dialogue>>(json);
        DialogueHolder dialogueHolder = new DialogueHolder();
        foreach(Dialogue dialogue in wrapper.items)
        {
            dialogueHolder.addDialogue(dialogue);
        }

        return dialogueHolder;
    }

    public DialogueHolder()
    {
        dialogueMap = new Dictionary<string, Dialogue>();
    }

    public Dialogue getDialog(string dialogueId)
    {
        return dialogueMap[dialogueId];
    }

    public string toJsonString()
    {
        Dialogue[] dialogueArray = new Dialogue[dialogueMap.Count];
        int count = 0;
        foreach(Dialogue dialogue in dialogueMap.Values)
        {
            dialogueArray[count++] = dialogue;
        }
        SerializableArrayWrapper<Dialogue> wrapper = new SerializableArrayWrapper<Dialogue>();
        wrapper.items = dialogueArray;
        return JsonUtility.ToJson(wrapper);
    }

    public void addDialogue(Dialogue dialogue)
    {
        dialogueMap[dialogue.id] = dialogue;
    }
}