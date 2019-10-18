using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    GUIStyle score_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle wait_style = new GUIStyle();
    GUIStyle bold_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    private bool game_start = false;       //游戏开始
    // Use this for initialization
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 30;
       score_style.normal.textColor = new Color(1,0,1,1);
        score_style.fontSize = 30;
        bold_style.normal.textColor = new Color(1, 0, 0);
        bold_style.fontSize = 30;
        over_style.normal.textColor = new Color(1, 0, 0);
        over_style.fontSize = 50;
        wait_style.normal.textColor = new Color(0, 0, 0);
        wait_style.fontSize = 30;
    }

    void Update()
    {
        if(game_start && !action.GetGameover())
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                action.Shoot();
            }
            float translationY = Input.GetAxis("Vertical");
            float translationX = Input.GetAxis("Horizontal");
            //移动弓箭
            action.MoveBow(translationX, translationY);
        }
    }
    private void OnGUI()
    {
        if(game_start)
        {
            if (!action.GetGameover())
            {
                GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
                GUI.Label(new Rect(85, 5, 200, 50), action.GetScore().ToString(), score_style);
               GUI.Label(new Rect(10, 40, 200, 50), "射箭: ", text_style);
                GUI.Label(new Rect(85, 40, 200, 50), action.GetCoolTimes2().ToString(), score_style);
            

                GUI.Label(new Rect(Screen.width - 400, 5, 50, 50), "弓箭数:", text_style);
                for (int i = 0; i < action.GetResidueNum(); i++)
                {
                    GUI.Label(new Rect(Screen.width - 300 + 30 * i, 0, 50, 50), "↑", bold_style);
                }
                for (int i = action.GetResidueNum(); i < 10; i++)
                {
                    GUI.Label(new Rect(Screen.width - 300 + 30 * i, 0, 50, 50), "↑", wait_style);
                }
                GUI.Label(new Rect(Screen.width - 270, 40, 200, 50), "风向: ", text_style);
                GUI.Label(new Rect(Screen.width - 200, 40, 200, 50), action.GetWind(), text_style);
            }

            if (action.GetGameover())
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, 110, 100, 100), "游戏结束", over_style);
                GUI.Label(new Rect(Screen.width / 2 - 80, 200, 50, 50), "分数:", text_style);
                GUI.Label(new Rect(Screen.width / 2 + 50, 200, 50, 50), action.GetScore().ToString(), score_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 60, 300, 100, 50), "重新开始"))
                {
                    action.Restart();
                    return;
                }
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 110, 110, 100, 100), "Shooting!", over_style);
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.width / 2 - 220, 400, 100), "方向键控制弓箭移动,Enter射箭", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 200, 100, 50), "游戏开始"))
            {
                game_start = true;
                action.BeginGame();
            }
        }
    }
}
