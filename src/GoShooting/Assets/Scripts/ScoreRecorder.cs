using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public int score;                   //分数
    public int target_arr;            //目标分数
    public int arrow_number;            //箭的数量
    public int this_score;
    void Start()
    {
        score = 0;
        target_arr = 5;
        arrow_number = 10;
    }
    //记录分数
    public void Record(GameObject disk)
    {
        this_score = disk.GetComponent<RingData>().score;
        score = this_score + score;
        //Debug.Log(score);
    }
}
