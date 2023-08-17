using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//前に進むスピード
	public float speed = 5.0f;
	//横に進むスピード
	public float slideSpeed = 2.0f;
	//ジャンプできる回数
	public int jumpCount = 1;
	//設定したジャンプの回数を保存できる変数
	int defaultJumpCount;
	//アニメーション
	Animator animator;
	//UIを管理するスクリプト
	UIManager uiscript;
	//上半身のコライダー用
	GameObject headCollider;

	Rigidbody playerRigidbody;


	void Start()
	{
		//変数に必要なデータを格納
		animator = GetComponent<Animator>();
		uiscript = GameObject.Find("Canvas").GetComponent<UIManager>();
		playerRigidbody = GetComponent<Rigidbody>();
		headCollider = GameObject.Find("HeadCollider");
		//設定したジャンプできる回数を保存する
		defaultJumpCount = jumpCount;
	}



	void Update()
	{
		//前に進む
		//Vector3はx軸、y軸、z軸がある。今回Unityちゃんが進むのがz軸
		transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

		//現在のX軸の位置を取得
		float posX = transform.position.x;

        //右アローキーを押した時
        if (Input.GetKey(KeyCode.RightArrow))
        {
			if(posX < 1.8f)
            {
				//ユニティチャンのX軸の位置が1.8より小さければ右に移動する
				transform.position += new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

		//左アローキーを押した時
		if (Input.GetKey(KeyCode.LeftArrow))
        {
			if(posX > -1.8f)
            {
				transform.position -= new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

        //アニメーション
        if (Input.GetKey(KeyCode.DownArrow))
        {
			animator.SetBool("Slide", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
			animator.SetBool("Slide", false);
        }

		//現在再生されているアニメーション情報を取得
		var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		//取得したアニメーションの名前が一致指定ればtrue
		bool isJump = stateInfo.IsName("Base Layer.Jump");
		bool isSlide = stateInfo.IsName("Base Layer.Slide");

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
			//速度を初期化
			playerRigidbody.velocity = new Vector3(0, 0, 0);
			//上方向に力を加える
			playerRigidbody.AddForce(new Vector3(0, 6, 0),ForceMode.Impulse);
			//ジャンプするアニメーションを再生
			animator.SetTrigger("Jump");
			//残りのジャンプ回数を減らす
			jumpCount--;
        }

        //スライディングしていたら頭の判定をなくす
        if (isSlide)
        {
			headCollider.SetActive(false);
        }
        else
        {
			headCollider.SetActive(true);
        }

		//落下時のGameOver判定
		if(transform.position.y <= -3)
        {
			uiscript.Gameover();
			animator.SetBool("Dead", true);
        }

	}

	// Triggerである障害物にぶつかったとき
	void OnTriggerEnter(Collider collider)
	{
		//ゴールした時
		if (collider.gameObject.tag == "Goal")
		{
			//速度を0にして進むのを止める
			speed = 0;
			//横移動する速度を0にして左右移動できなくする
			slideSpeed = 0;
			//ゴールをしたら正面を向くようにする
			Vector3 lockpos = Camera.main.transform.position;
			lockpos.y = transform.position.y;
			transform.LookAt(lockpos);

			//アニメーション
			animator.SetBool("Win", true);

			//UIの表示
			uiscript.Goal();
			
		}
	}


    //Triggerでない障害物にぶつかったとき
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Barrier")
        {
			//速度を0にして進むのを止める
			speed = 0;
			//横移動する速度を0にして左右移動できなくする
			slideSpeed = 0;
			//アニメーション
			animator.SetBool("Dead", true);
			//UIの表示
			uiscript.Gameover();
        }

		//地面についた時にジャンプの回数を初期値に戻す
		if(collision.gameObject.tag == "Ground")
        {
			jumpCount = defaultJumpCount;
        }
    }
}
