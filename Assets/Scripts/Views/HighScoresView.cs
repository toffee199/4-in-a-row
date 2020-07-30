using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresView : MonoBehaviour
{
    public void Init(List<HighScoreItemData> highScores)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach(var highScore in highScores)
        {
            var item = Instantiate(Resources.Load("Prefabs/HighScoreItem")) as GameObject;
            var highScoreItemView = item.GetComponent<HighScoreItemView>();
            highScoreItemView.Init(highScore.GetDisplayName(), highScore.GetScore());
            item.transform.SetParent(transform);
            item.transform.localScale = Vector3.one;
        }
    }
}
