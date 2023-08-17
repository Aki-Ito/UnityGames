using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //Playerが動くスピード
    float speed = 5.0f;
    //Playerのジャンプ力
    float jumpForce = 500.0f;
    //ジャンプできる回数
    public int jumpCount;
    //設定したジャンプできる変数を保存
    int defaultJumpCount;
    //GameOverTextオブジェクトを入れるための変数
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        //設定したジャンプできる回数を保存
        defaultJumpCount = jumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        //もし右矢印キーが押されているなら
        //Inputはユーザの入力情報へとアクセスする。どのキーが押されたかなどを判定する
        if(Input.GetKey(KeyCode.RightArrow)){
            //右に移動する
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount==0)
        {
            //ジャンプできる回数を減らす
            jumpCount--;
            //AddForceがジャンプを意味している。
            //Vector3.up * jumpForceどの方向にどれくらい力を入れるか
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }

        if(this.gameObject.transform.position.y < -5.0f)
        {
            //GameOverテキストを表示する
            gameOverText.gameObject.SetActive(true);
        }

    }

    //playerがgameObjectにぶつかった時
    private void OnCollisionEnter(Collision collision)
    {
        //地面に衝突した時のジャンプの回数を初期値に戻す
        if(collision.gameObject.tag == "Ground")
        {
            jumpCount = defaultJumpCount;
        }
    }
}
