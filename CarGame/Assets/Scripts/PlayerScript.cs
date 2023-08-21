using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool movesForward;
    bool movesBack;

    float moveSpeed = 10.0f;
    float rotateSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //マイフレームごとに実行される
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime);
        }
        //上矢印キーを押したらtrueに
        movesForward = Input.GetKey(KeyCode.UpArrow);
        //下矢印キーを押したらtrueに
        movesBack = Input.GetKey(KeyCode.DownArrow);
    }

    //一定時間ごとに実行される。初期設定は0.02秒
    private void FixedUpdate()
    {
        if (movesForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }else if (movesBack)
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
