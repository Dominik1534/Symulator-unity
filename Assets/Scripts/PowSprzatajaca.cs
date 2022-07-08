using UnityEngine;
using UnityEngine.UI;

public class PowSprzatajaca : MonoBehaviour
{
    float iloscBrudu = 1;
    public Text procentWyczyszczenia;
    float iloscPosprzatanegoBrudu = 1;
    private bool srt;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Brud")
        {
            iloscPosprzatanegoBrudu++;
            Destroy(other.gameObject);

        }
    }
    public void startbutton()
    {
        srt = true;
    }
    public void Zliczbrud()
    {

        iloscBrudu = GameObject.FindGameObjectsWithTag("Brud").Length / 2;
    }
    public float procPosprzatanegoBrudu()
    {
        float proc;
        proc = iloscPosprzatanegoBrudu / iloscBrudu * 100;
        return proc;
    }
    void Update()
    {
        if (srt)
        {
            procentWyczyszczenia.text = "Procent posprzatanego brudu: " + procPosprzatanegoBrudu().ToString("N3") + "%";
        }
    }
}
