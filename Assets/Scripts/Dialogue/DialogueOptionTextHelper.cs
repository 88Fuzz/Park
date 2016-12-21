using UnityEngine.UI;

/*
 * Wrapper around the UI Text component, used to set the text, color, and visibility of the text options on screen.
 */
public class DialogueOptionTextHelper
{
    private static readonly string DIALOGUE_OPTION_SEPERATOR = ". ";
    private static readonly UnityEngine.Color DESELECTED_COLOR = UnityEngine.Color.black;
    private static readonly UnityEngine.Color SELECTED_COLOR = UnityEngine.Color.yellow;
    private Text text;
    private int choiceNumber;

    public DialogueOptionTextHelper(Text text, int choiceNumber)
    {
        this.text = text;
        this.choiceNumber = choiceNumber;
        enableText(false);
        deselected();
    }

    public void updateText(string newText)
    {
        text.text = choiceNumber + DIALOGUE_OPTION_SEPERATOR + newText;
    }

    public void enableText(bool enable)
    {
        text.enabled = enable;
        deselected();
    }

    public void selected()
    {
        text.color = SELECTED_COLOR;
    }

    public void deselected()
    {
        text.color = DESELECTED_COLOR;
    }
}