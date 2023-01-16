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

    [SerializeField] public uint timeLimit_sec = 60;

    [NonSerialized] public uint currentTime;
    public Text _countText;

    private void Start()
    {
        _countText.text = timeLimit_sec.ToString();
    }
    /// <summary>
    /// ゲームのカウントダウンを開始する
    /// </summary>
    /// <param name="timeLimit_sec">制限時間</param>
    /// 
    public void StartTimer()
    {
        soundManager.playStartSound();
        startObj.SetActive(true);
        StartCoroutine(timeCoroutine(timeLimit_sec));
    }

    IEnumerator timeCoroutine(uint time)
    {
        for (currentTime = time; currentTime > 0; currentTime--)
        {
            // 残り時間を画面左上のタイマー表示に反映
            _countText.text = currentTime.ToString();
            // 残り時間が少なるに連れて段階的にフルーツの落下頻度が上昇いていく
            var tmp = Mathf.Pow(20, (timeLimit_sec - currentTime) / (float)timeLimit_sec);
            Debug.Log(tmp);
            fallManager.rate = tmp;

            yield return new WaitForSeconds(1.0f);
        }
        // タイムアップ時の処理
        _countText.text = "TIMEUP!";
        soundManager.playFinishSound();
        finishObj.SetActive(true);
        GameState.state = GameState.statusList.Finished;

        // 5秒後にシーン遷移する
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