using UnityEngine;

[System.Serializable]
public class DeckFormat
{
    public string name;
    public int count;

    public DeckFormat(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
}
