﻿/*
 * Wrapper class around the Input keys.
 */ 
public class InputButton
{
    private static object objectLock = new object();
    private static int count = 0;
    private int id;
    private string action;

    public InputButton(string action)
    {
        lock (objectLock)
        {
            id = count++;
        }
        this.action = action;
    }

    public int Id
    {
        get { return id; }
    }

    public string Action
    {
        get { return action; }
    }

    //Attacks
    public static readonly InputButton PRIMARY_ATTACK = new InputButton("Fire1");
    public static readonly InputButton SECONDARY_ATTACK = new InputButton("Fire2");
    //Movements
    public static readonly InputButton HORIZONTAL = new InputButton("Horizontal");
    public static readonly InputButton FORWARD = new InputButton("Vertical");
    public static readonly InputButton DASH = new InputButton("Dash");
    public static readonly InputButton JUMP = new InputButton("Jump");
    //Mouse
    public static readonly InputButton MOUSE_X = new InputButton("Mouse X");
    public static readonly InputButton MOUSE_Y = new InputButton("Mouse Y");
    //Miscellaneous
    public static readonly InputButton RANDOM = new InputButton("Random");
    public static readonly InputButton NONE = new InputButton("N/A");
    //DialogueOption Selection
    public static readonly InputButton DIALOGUE_UP = new InputButton("DialogueUp");
    public static readonly InputButton DIALOGUE_DOWN = new InputButton("DialogueDown");
    public static readonly InputButton DIALOGUE_SELECT = new InputButton("DialogueSelect");

    public static readonly InputButton DIALOGUE_ONE = new InputButton("DialogueOne");
    public static readonly InputButton DIALOGUE_TWO = new InputButton("DialogueTwo");
    public static readonly InputButton DIALOGUE_THREE = new InputButton("DialogueThree");
    public static readonly InputButton DIALOGUE_FOUR = new InputButton("DialogueFour");
}