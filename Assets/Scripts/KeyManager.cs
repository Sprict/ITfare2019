using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer = null;
    [SerializeField]
    private FallManager fallmanager = null;
    [SerializeField]
    private GameObject setumei;
    public AudioClip open;
    public AudioClip close;
    private AudioSource audioSource;

    public ScoreManager scoreManager;
    // Update is called once per frame

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
        // シーンを再読み込み
        else if (Input.GetKeyDown(KeyCode.R))
        {
            moveScene(SceneManager.GetActiveScene().name);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && GameState.state == GameState.statusList.PreStart)
        {
            GameState.state = GameState.statusList.Started;
            timer.StartTimer();
            fallmanager.StartFall();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (setumei.activeSelf == false)
            {
                audioSource.PlayOneShot(open);
                Invoke("Open", 0.55f);
            }
            else
            {
                audioSource.PlayOneShot(close);
                Invoke("Close", 1.0f);
            }
        }
    }

    void Open()
    {
        setumei.SetActive(true);
    }

    void Close()
    {
        setumei.SetActive(false);
    }

    void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    void moveScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
