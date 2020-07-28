using System;
using System.Collections.Generic;
using UnityEngine;

public class HigheScoreModel
{
    private List<HighScoreItemData> highScores;
    const string HIGH_SCORE_PREFS = "High Scores";

    public HigheScoreModel()
    {
        string highScoreJson = PlayerPrefs.GetString(HIGH_SCORE_PREFS, null);
        if (!string.IsNullOrEmpty(highScoreJson)) {
            highScores = JsonHelper.FromJson<HighScoreItemData>(highScoreJson);
        }
        if (highScores == null) {
            highScores = new List<HighScoreItemData>();
        }
    }

    public void SaveHighScore(int score, string name)
    {
        HighScoreItemData newHighScore = new HighScoreItemData(name, score);
        highScores.Add(newHighScore);
        string highScoreJson = JsonHelper.ToJson(highScores);
        PlayerPrefs.SetString(HIGH_SCORE_PREFS, highScoreJson);
    }

    public List<HighScoreItemData> GetTopHighScores()
    {
        List<HighScoreItemData> top5Scores = new List<HighScoreItemData>();
        highScores.Sort((x, y) => y.score.CompareTo(x.score));
        int i = 0;
        while (i < 5 && i < highScores.Count)
        {
            top5Scores.Add(highScores[i]);
            i++;
        }
        return top5Scores;
    }
}


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