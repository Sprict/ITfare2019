using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName ="Ranking", menuName ="CreateRanking")]
public class ranking
{
    string RANKING_PREF_KEY = "ranking";
    int RANKING_NUM = 10;
    float[] rank;

    ranking()
    {
        rank = new float[RANKING_NUM];
    }

    void getRanking()
    {
        var _ranking = PlayerPrefs.GetString(RANKING_PREF_KEY);
        if(_ranking.Length > 0)
        {

        }
    }
}

class record
{
    public string name;
    public int score;
}
