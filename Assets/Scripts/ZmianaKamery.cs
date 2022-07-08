using UnityEngine;

public class ZmianaKamery : MonoBehaviour
{
    public Camera kameraOdkurzacz;
    public Camera kameraMapa;

    public void WidokOdkurzacz()
    {
        kameraMapa.enabled = false;
        kameraOdkurzacz.enabled = true;
    }


    public void WidokMapa()
    {
        kameraMapa.enabled = true;
        kameraOdkurzacz.enabled = false;

    }
}

