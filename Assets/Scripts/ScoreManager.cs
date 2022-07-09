using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
public class ScoreManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1440, 1080, true, 60);

    }
    [SerializeField]
    private catcher catcher;
    private int resultscore = 0;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Text ranking;
    public InputField inputField;
    public Text resultText;
    public Text text;
    private string[] _preName = new string[100];
    private int[] _prescore = new int[100];
    private string _playerName;

    [SerializeField]
    private GameObject kakunin;
    Dictionary<string, int> records;
     private void Start()
    {
        records= new Dictionary<string, int>();
    }




    void getScores(string[] _preName, int[] _prescore)
    {
        for (int i = 0; i < 100; i++)
        {
            _preName[i] = PlayerPrefs.GetString("SCORE" + i);
            _prescore[i] = PlayerPrefs.GetInt("SCORE" + i, 0);
        }
    }

    void getRanking()
    {
        IOrderedEnumerable<KeyValuePair<string, int>> table_1 =
            records.OrderByDescending(selector => { return selector.Value; });
    }

    void addRanking(int score, int[] _prescore)
    {
        for(int i = 0; i < 100; i++)
        {
            if(score > _prescore[i])
            {
                PlayerPrefs.SetInt("SCORE" + i, 0);
                PlayerPrefs.SetString("SCORE"+i, _playerName);
            }
        }
    }
    bool addRecord(int score, string name)
    {
        if (records.ContainsKey(name))
        {
            kakunin.GetComponentInChildren<Text>().text = _playerName + "は" + records[_playerName] + "点で記録されているけれど上書きする？";
            kakunin.SetActive(true);
            return false;
        }
        records.Add(name, score);
        return true;
    }

    bool checkName(string name)
    {
        if(records.ContainsKey(name))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool Kakunin()
    {
        kakunin.SetActive(true);

        return false;
    }
    public void closeKakunin()
    {
        kakunin.SetActive(false);
    }

    void deleteScore(string name)
    {
        PlayerPrefs.DeleteKey(name);
    }

    public void InputText()
    {
        text.text = inputField.text;
    }


    public void finishinputField()
    {
        _playerName = inputField.text;

        if(addRecord(resultscore, _playerName))
        {
            string inputText = "\n\t\t\t\t\t\t\t\tランキング\n\n";
            var hogehoge = records.OrderByDescending((selector) => { return selector.Value; });
            int j = 0;
            int rank = 0;
            int preScore = -1;
            foreach (var hoge in hogehoge)
            {
                SaveDataManager.saveData.score[j] = hoge.Value;
                SaveDataManager.saveData.name[j] = hoge.Key;
                if(hoge.Value == preScore)
                {
                  rank -= 1;
                }
                inputText += "\t" + (++rank) + "\t" + PaddingInBytes(hoge.Key, 14) + "\t\t\t"+hoge.Value + "\t点\n";
                preScore = hoge.Value;
                ++j;
            }
            SaveDataManager.Save();
            ranking.text = inputText;
            inputField.transform.parent.GetComponent<Animator>().SetTrigger("ranking");
            ranking.transform.parent.gameObject.SetActive(true);
        }

        
    }

    public void uwagaki()
    {
        kakunin.SetActive(false);
        records.Remove(_playerName);
        finishinputField();
    }

    public void startResult()
    {
        resultscore = catcher.localScore;
        SaveDataManager.Init();
        var score = SaveDataManager.saveData.score;
        var name = SaveDataManager.saveData.name;
        for (int i = 0; i < 100 || score[i] == 0; i++)
        {
            if (records.ContainsKey(name[i]))
                break;
            records.Add(name[i], score[i]);
        }

        inputField.transform.parent.gameObject.SetActive(true);
        inputField.ActivateInputField();
        Invoke("showResult", 2.5f);
    }
    public void showResult()
    {
        resultText.text = Convert.ToString(resultscore);
    }


    public string PaddingInBytes(string moji, int byteCount)
    {
        Encoding enc = Encoding.GetEncoding("Shift_JIS");

        /*
        if (byteCount < enc.GetByteCount(moji))
        {
            // valueが既定のバイト数を超えている場合は、切り落とし
            moji = moji.Substring(0, byteCount);
        }
        */

        return moji.PadRight(byteCount - (enc.GetByteCount(moji) - moji.Length));

    }
}
