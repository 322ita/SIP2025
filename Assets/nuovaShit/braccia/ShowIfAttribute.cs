using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public string boolName;
    public bool showWhenTrue;

    public ShowIfAttribute(string boolName, bool showWhenTrue = true)
    {
        this.boolName = boolName;
        this.showWhenTrue = showWhenTrue;
    }
}
