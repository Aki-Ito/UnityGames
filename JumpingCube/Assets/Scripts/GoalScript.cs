using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UIを使う時に必要な宣言
using UnityEngine.UI;

public class GoalScript : MonoBehaviour
{
    //GoalTextを入れるための変数
    public Text goalText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //GoalTextを表示する
            goalText.gameObject.SetActive(true);
        }
    }

}
