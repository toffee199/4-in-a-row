using System;

[Serializable]
public class HighScoreItemData
{
    public string displayName;
    public int score;

    public HighScoreItemData(string name, int score)
    {
        this.displayName = name;
        this.score = score;
    }
}