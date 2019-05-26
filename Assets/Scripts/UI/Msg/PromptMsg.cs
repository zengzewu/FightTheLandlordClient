using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptMsg
{

    public string Text { get; set; }
    public Color color { get; set; }

    public PromptMsg()
    {

    }

    public PromptMsg(string text, Color color)
    {
        this.Text = text;
        this.color = color;
    }

    public void Change(string text, Color color)
    {
        this.Text = text;
        this.color = color;
    }
}

