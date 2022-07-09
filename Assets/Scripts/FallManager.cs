using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallManager : MonoBehaviour
{
    public Fruit[] fruits;
    public Timer timer;

    public float fallAreaLeft;
    public float fallAreaRight;
    public float rate = 1.0f;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }
    public void StartFall()
    {
        StartCoroutine(Faller(rate));
    }

    public IEnumerator Faller(float waittime)
    {
        do
        {
            float insPosx = Random.Range(fallAreaLeft, fallAreaRight);
            int selected_fruit = SelectFruit();
            Instantiate(fruits[selected_fruit].fruit, new Vector3(insPosx, 10.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(this.rate);
        } while (GameState.state == GameState.statusList.Started);
    }
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

