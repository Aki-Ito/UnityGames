using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapScript : MonoBehaviour
{
    int checkCount = 0;
    int lapCount = 1;

    //View用の変数
    public GameObject viewObject;
    //ViewScript用の変数
    ViewScript viewScript;
    // Start is called before the first frame update
    void Start()
    {
        //変数viewScriptに、ViewオブジェクトにアタッチされているViewScriptを代入する
        //変数に実際にViewScriptインスタンスを代入する
        viewScript = viewObject.GetComponent<ViewScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CheckPoint1")
        {
            if(checkCount == 0)
            {
                checkCount++;
            }
        }else if(other.gameObject.name == "CheckPoint2")
        {
            if(checkCount == 1)
            {
                checkCount++;
            }
        }else if(other.gameObject.name == "CheckPoint3")
        {
            if(checkCount == 2)
            {
                checkCount++;
            }
        }
        else if(other.gameObject.name == "StartGoalChecker")
        {
            if (checkCount != 3)
            {
                viewScript.ShowLapCount(lapCount);
                Debug.Log("lapCount:"+lapCount);
                return;
           }else if(checkCount == 3)
            {
                if(lapCount == 1 || lapCount == 2)
                {
                    //1周目、2周目が終わったらlapCountを追加してコンソールに表示
                    lapCount++;
                    viewScript.ShowLapCount(lapCount);
                    Debug.Log("lapCount" + lapCount);
                    checkCount = 0;
                    return;
                }
                else if(lapCount == 3)
                {
                    //3周目はゴールを表示！
                    viewScript.ShowGoalText("Goal!");
                    Debug.Log("Gaol!");
                }
            }
        }
    }
}
