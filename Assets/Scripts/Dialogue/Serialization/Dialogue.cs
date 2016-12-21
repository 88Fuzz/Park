using System;

/*
 * Dialogue is any text presented on screen that will propt a response from the user. Currently all Dialogue must result in a response, 
 * but that can be easily changed.
 */
[Serializable]
public class Dialogue
{
    public String id;
    public String text;
    /*Two ways of doing this.
    1) keep this as is and have a method to construct the nextDialog after reading in the serialization files.
    2) Have this thing only keep track of the nextDialogIds and then when needed, have a look up to find the object that corresponds to the nextDialogId

        As of writing this conversation, 2 is the approach I'm implementing with the DialogueHolder class
    */
    public String[] dialogueOptionIds;
}