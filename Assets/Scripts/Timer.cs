using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private FallManager fallManager;

    [SerializeField] private GameObject startObj = null;
    [SerializeField] private GameObject finishObj = null;
    [SerializeField] private GameObject[] image;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private int timeLimit = 60;
    public Text _countText;


    /// <summary>
    /// ゲームのカウントダウンを開始する
    /// </summary>
    /// <param name="timeLimit">制限時間</param>
    /// 
    public void StartTimer(int timeLimit)
    {
        soundManager.playStartSound();
        startObj.SetActive(true);
        this.timeLimit = timeLimit;
        StartCoroutine(Example(1.0f));
    }

    IEnumerator Example(float time)
    {
        for (int i = timeLimit; i >= 0; i--)
        {
            print(i);
            _countText.text = Convert.ToString(i);
            if (i == 15 || i == 30)
            {
                fallManager.rate /= 2.0f;
            }
            yield return new WaitForSeconds(1.0f);
        }
        _countText.text = "finish";
        soundManager.playFinishSound();
        finishObj.SetActive(true);
        GameState.state = GameState.statusList.Finished;
        // 5病後にシーン遷移
        Invoke("changeScene", 5.0f);
        yield return null;
    }

    void changeScene()
    {
        StartCoroutine("changeSceneCroutine", 1.0f);
        Destroy(GameObject.Find("player1"));
        Destroy(GameObject.Find("kago"));


    }

    IEnumerator changeSceneCroutine(float time)
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(image[i]);
            if(i == 1)
            {
                soundManager.playDururuSound();
            }
            yield return new WaitForSeconds(time);
        }
        StartCoroutine("showSound", 2.5f);
        scoreManager.startResult();
        yield return null;
    }

    IEnumerator showSound(float time)
    {
        yield return new WaitForSeconds(time);
        soundManager.playCheerSound();
        yield return null;
    }
}