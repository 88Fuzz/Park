using System;

/*
 * DialogueOption is any text that is presented to the user as an option to pick. 
 */
[Serializable]
public class DialogueOption
{
    public String id;
    public String text;
    /*Two ways of doing this.
    1) keep this as is and have a method to construct the nextDialog after reading in the serialization files.
    2) Have this thing only keep track of the nextDialogIds and then when needed, have a look up to find the object that corresponds to the nextDialogId

        As of writing this conversation, 2 is the approach I'm implementing
    */
    public String nextDialogId;
}