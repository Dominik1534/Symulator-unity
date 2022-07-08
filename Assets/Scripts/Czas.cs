using UnityEngine;
using UnityEngine.UI;

public class Czas : MonoBehaviour
{
    float val;
    bool srt, stp, rst;
    public Text czasText;
    public Button przycisk;

    void Start()
    {
        val = 0;
        srt = false;
        stp = false;
        rst = false;

    }
    void Update()
    {
        if (srt)
        {
            val += Time.deltaTime;
        }
        float minutes = Mathf.FloorToInt(val / 60);
        float seconds = Mathf.FloorToInt(val % 60);
        czasText.text = "Czas: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        if (minutes >= 30)
        {
            
            przycisk.onClick.Invoke();
        }
    }
    public void stopbutton()
    {
        srt = false;
    }
    public void resetbutton()
    {
        srt = false;
        val = 0;
    }
    public void startbutton()
    {
        srt = true;
    }
}