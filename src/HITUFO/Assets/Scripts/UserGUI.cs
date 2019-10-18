using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    public int life = 6;                   //血量
    //每个GUI的style
    GUIStyle bold_style = new GUIStyle();
    GUIStyle score_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    GUIStyle time_style = new GUIStyle();
    GUIStyle wait_style = new GUIStyle();
    private int high_score = 0;            //最高分
    private bool game_start = false;       //游戏开始
        private bool game_wait = true; 

    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }
	
	void OnGUI ()
    {
        bold_style.normal.textColor = new Color(1, 0, 0);
        bold_style.fontSize = 30;
        text_style.normal.textColor = new Color(0,0,0, 1);
        text_style.fontSize = 30;
        score_style.normal.textColor = new Color(1,0,1,1);
        score_style.fontSize = 30;
        over_style.normal.textColor = new Color(1, 0, 0);
        over_style.fontSize = 50;
        wait_style.normal.textColor = new Color(0, 0, 0);
        wait_style.fontSize = 30;
        time_style.normal.textColor = new Color(1, 0, 0);
        time_style.fontSize = 100;

        if (game_start)
        {
            //用户射击
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                action.Hit(pos);
            }

            GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
            GUI.Label(new Rect(85, 5, 200, 50), action.GetScore().ToString(), score_style);

            GUI.Label(new Rect(Screen.width - 280, 5, 50, 50), "生命:", text_style);
            //显示当前血量
            for (int i = 0; i < life; i++)
            {
                GUI.Label(new Rect(Screen.width - 200 + 30 * i, 5, 50, 50), "❤️", bold_style);
            }
            for (int i = life; i < 6; i++)
            {
                GUI.Label(new Rect(Screen.width - 200 + 30 * i, 5, 50, 50), "❤️", wait_style);
            }

            if (action.GetCoolTimes() >= 0 && action.GetRound() == 1) {
                        //    GUI.Label(new Rect(Screen.width / 2 - 100, 60, 200, 50), "", wait_style);
                    if (action.GetCoolTimes() == 0) {
                        GUI.Label(new Rect(Screen.width / 2 - 200, 150, 200, 50), "ROUND 1", time_style);
                    }else
                        GUI.Label(new Rect(Screen.width / 2 - 30, 150, 200, 50), action.GetCoolTimes().ToString(), time_style);


            }
            if (action.GetRound() == 2 && action.GetCoolTimes2() > 0) {
                    GUI.Label(new Rect(Screen.width / 2 - 200, 150, 200, 50), "ROUND 2", time_style);

            }
            if (action.GetRound() == 3 && action.GetCoolTimes3() > 0) {
                    GUI.Label(new Rect(Screen.width / 2 - 200, 150, 200, 50), "ROUND 3", time_style);

            }


            //游戏结束
            if (life == 0)
            {
                high_score = high_score > action.GetScore() ? high_score : action.GetScore();
                GUI.Label(new Rect(Screen.width / 2 - 100, 110, 100, 100), "游戏结束", over_style);
                GUI.Label(new Rect(Screen.width / 2 - 80, 200, 50, 50), "最高分:", text_style);
                GUI.Label(new Rect(Screen.width / 2 + 50, 200, 50, 50), high_score.ToString(), text_style);

                if (GUI.Button(new Rect(Screen.width / 2 - 60, 300, 100, 50), "重新开始"))
                {
                    life = 6;
                    action.ReStart();
                    return;
                }
                action.GameOver();
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 110, 110, 100, 100), "HIT!UFO!", over_style);
        //    GUI.Label(new Rect(Screen.width / 2 - 150, Screen.width / 2 - 220, 400, 100), "大量UFO出现，点击它们，即可消灭，快来加入战斗吧", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 200, 100, 50), "游戏开始"))
            {
                game_start = true;
                
                action.BeginGame();
            }
        }
    }
    public void ReduceBlood()
    {
        if(life > 0)
            life--;
    }
}
