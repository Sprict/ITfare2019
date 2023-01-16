using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallManager : MonoBehaviour
{
    public Timer timer;
    public float rate  = 1.0f;

    [SerializeField] private float AreaLimitLeft = -11.0f;
    [SerializeField] private float AreaLimitRight = 11.0f;

    [SerializeField] private Fruit[] fruits;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }
    public void StartFall()
    {
        StartCoroutine(Faller());
    }

    /// <summary>
    /// 毎秒rate個頻度でランダムな位置にフルーツを生成する
    /// </summary>
    /// <param name="rate">フルーツを生成する頻度（秒）</param>
    /// <returns></returns>
    public IEnumerator Faller()
    {
        do
        {
            float insPosx = Random.Range(AreaLimitLeft, AreaLimitRight);
            int selected_fruit = SelectFruit();
            Instantiate(fruits[selected_fruit].fruit, new Vector3(insPosx, 10.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(1/rate);
        } while (GameState.state == GameState.statusList.Started);
    }

    /// <summary>
    /// 種類ごとのフルーツの出現率を考慮してランダムに選ばれたフルーツを返す
    /// </summary>
    /// <returns></returns>
    int SelectFruit()
    {
        var arr = new List<int>();
        for(int i = 0; i < fruits.Length; i++)
        {
            for(int j = 0; j < fruits[i].apRate; j++)
            {
                arr.Add(i);
            }
        }
        int num = Random.Range(0, arr.Count);
        return arr[num];
    }
}

[System.Serializable]
public class Fruit
{
    public GameObject fruit;

    [Header("出現率"),Range(0,100)]
    public int apRate; 
}

