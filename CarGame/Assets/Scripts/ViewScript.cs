using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScript : MonoBehaviour
{
    public Text lapCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLapCount(int count)
    {
        lapCountText.text = "LapCount:" + count.ToString();
    }

    public void ShowGoalText(string goal)
    {
        lapCountText.text = goal;
    }
}
