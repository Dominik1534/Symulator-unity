using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Siatka : MonoBehaviour
{
    System.Random random = new System.Random();
    [SerializeField] public Transform Sciany;
    [SerializeField] private Transform Tekst;
    [SerializeField] private Transform CelOdkurzacza;
    public NavMeshSurface surfance;
    public Dropdown WyborAlgorytmu;
    public Dropdown WyborPlanszy;
    public Text polaDoWyczyszczenia;
    public Text polaWyczyszczone;
    public Text polaWyczyszczoneProc;
    public Text aktualnyAlgorytm;
    public Text ponownePrzejscia;
    public Button przycisk;
    public SiatkaXZ<SiatkaObiektow> siatka;
    public Transform odkurzacz;
    public GameObject droga;
    public GameObject partner;
    public Vector3 pozStart;

    private bool CzyKolizja;
    private int px;
    private int pz;
    private int ruch;

    private float czasTargetu;
    private bool timerIsRunning = false;
    private int obszar;
    public int rozmiarSiatki;
    private bool KoniecSprzatania = false;
    private int B = 1;
    private bool RLewo = false;
    private bool RPrawo = false;
    private bool Inicjacja = true;
    private bool Krok1 = false;
    private bool Krok2 = false;
    private bool Krok3 = false;
    private bool Krok4 = false;
    public bool start = false;
    private int ponownePrzejsciaPola = 0;

    public void Kierunek(int kierunek)
    {

        //dó³
        if (kierunek == 4)
        {
            pz--;
        }
        //lewo
        if (kierunek == 3)
        {
            px--;
        }
        //góra
        if (kierunek == 2)
        {
            pz++;
        }
        //prawo
        if (kierunek == 1)
        {
            px++;
        }
        //prawy dó³
        if (kierunek == 5)
        {
            px++;
            pz--;
        }
        //lewy do³
        if (kierunek == 6)
        {
            px--;
            pz--;
        }
        if (kierunek == 7)
        {
            //lewy góra

            px--;

            pz++;
        }
        if (kierunek == 8)
        {
            //prawa góta

            px++;

            pz++;
        }


    }
    public bool SprawdzKolejnyRuchCzyPuste(int ruch)
    {
        if (ruch == 1 && siatka.GetGridObject(px + 1, pz).czySciana != true) { return true; }
        if (ruch == 2 && siatka.GetGridObject(px, pz + 1).czySciana != true) { return true; }
        if (ruch == 3 && siatka.GetGridObject(px - 1, pz).czySciana != true) { return true; }
        if (ruch == 4 && siatka.GetGridObject(px, pz - 1).czySciana != true) { return true; }

        if (ruch == 5 && siatka.GetGridObject(px + 1, pz - 1).czySciana != true) { return true; }
        if (ruch == 6 && siatka.GetGridObject(px - 1, pz - 1).czySciana != true) { return true; }
        if (ruch == 7 && siatka.GetGridObject(px - 1, pz + 1).czySciana != true) { return true; }
        if (ruch == 8 && siatka.GetGridObject(px + 1, pz + 1).czySciana != true) { return true; }
        return false;
    }
    public bool SprawdzKolejnyRuchCzySciana(int ruch)
    {
        if (ruch == 1 && siatka.GetGridObject(px + 1, pz).czySciana == true) { return true; }
        if (ruch == 2 && siatka.GetGridObject(px, pz + 1).czySciana == true) { return true; }
        if (ruch == 3 && siatka.GetGridObject(px - 1, pz).czySciana == true) { return true; }
        if (ruch == 4 && siatka.GetGridObject(px, pz - 1).czySciana == true) { return true; }

        if (ruch == 5 && siatka.GetGridObject(px + 1, pz - 1).czySciana == true) { return true; }
        if (ruch == 6 && siatka.GetGridObject(px - 1, pz - 1).czySciana == true) { return true; }
        if (ruch == 7 && siatka.GetGridObject(px - 1, pz + 1).czySciana == true) { return true; }
        if (ruch == 8 && siatka.GetGridObject(px + 1, pz + 1).czySciana == true) { return true; }

        return false;
    }
    public bool SprawdzKolejnyRuchCzyTrasa(int ruch)
    {

        if (ruch == 1 && siatka.GetGridObject(px + 1, pz).liczbaPrzejsc > 10 && siatka.GetGridObject(px + 1, pz).czySciana != true) { return true; }
        if (ruch == 2 && siatka.GetGridObject(px, pz + 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px, pz + 1).czySciana != true) { return true; }
        if (ruch == 3 && siatka.GetGridObject(px - 1, pz).liczbaPrzejsc > 10 && siatka.GetGridObject(px - 1, pz).czySciana != true) { return true; }
        if (ruch == 4 && siatka.GetGridObject(px, pz - 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px, pz - 1).czySciana != true) { return true; }

        if (ruch == 5 && siatka.GetGridObject(px + 1, pz - 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px + 1, pz - 1).czySciana != true) { return true; }
        if (ruch == 6 && siatka.GetGridObject(px - 1, pz - 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px - 1, pz - 1).czySciana != true) { return true; }
        if (ruch == 7 && siatka.GetGridObject(px - 1, pz + 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px - 1, pz + 1).czySciana != true) { return true; }
        if (ruch == 8 && siatka.GetGridObject(px + 1, pz + 1).liczbaPrzejsc > 10 && siatka.GetGridObject(px + 1, pz + 1).czySciana != true) { return true; }


        return false;
    }
    //public bool SprawdzKolejnyRuchCzyPusteBezScian(int ruch)
    //{
    //    if (ruch == 1 && siatka.GetGridObject(px + 1, pz).czyPosprzatane != true && siatka.GetGridObject(px + 1, pz).czySciana != true) { return true; }
    //    if (ruch == 2 && siatka.GetGridObject(px, pz + 1).czyPosprzatane != true && siatka.GetGridObject(px, pz + 1).czySciana != true) { return true; }
    //    if (ruch == 3 && siatka.GetGridObject(px - 1, pz).czyPosprzatane != true && siatka.GetGridObject(px - 1, pz).czySciana != true) { return true; }
    //    if (ruch == 4 && siatka.GetGridObject(px, pz - 1).czyPosprzatane != true && siatka.GetGridObject(px, pz - 1).czySciana != true) { return true; }

    //    if (ruch == 5 && siatka.GetGridObject(px + 1, pz - 1).czyPosprzatane != true && siatka.GetGridObject(px + 1, pz - 1).czySciana != true) { return true; }
    //    if (ruch == 6 && siatka.GetGridObject(px - 1, pz - 1).czyPosprzatane != true && siatka.GetGridObject(px - 1, pz - 1).czySciana != true) { return true; }
    //    if (ruch == 7 && siatka.GetGridObject(px - 1, pz + 1).czyPosprzatane != true && siatka.GetGridObject(px - 1, pz + 1).czySciana != true) { return true; }
    //    if (ruch == 8 && siatka.GetGridObject(px + 1, pz + 1).czyPosprzatane != true && siatka.GetGridObject(px + 1, pz + 1).czySciana != true) { return true; }



    //    return false;
    //}

    public bool SprawdzCzyPunktGraniczyZTrasa(int xp, int zp)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (i == 1 && siatka.GetGridObject(xp, zp + 1).czyPosprzatane == true && siatka.GetGridObject(xp, zp + 1).czySciana != true) { return true; }
            if (i == 2 && siatka.GetGridObject(xp + 1, zp).czyPosprzatane == true && siatka.GetGridObject(xp + 1, zp).czySciana != true) { return true; }
            if (i == 3 && siatka.GetGridObject(xp, zp - 1).czyPosprzatane == true && siatka.GetGridObject(xp, zp - 1).czySciana != true) { return true; }
            if (i == 4 && siatka.GetGridObject(xp - 1, zp).czyPosprzatane == true && siatka.GetGridObject(xp - 1, zp).czySciana != true) { return true; }

            if (i == 5 && siatka.GetGridObject(xp - 1, zp + 1).czyPosprzatane == true && siatka.GetGridObject(xp - 1, zp + 1).czySciana != true) { return true; }
            if (i == 6 && siatka.GetGridObject(xp - 1, zp - 1).czyPosprzatane == true && siatka.GetGridObject(xp - 1, zp - 1).czySciana != true) { return true; }
            if (i == 7 && siatka.GetGridObject(xp + 1, zp - 1).czyPosprzatane == true && siatka.GetGridObject(xp + 1, zp - 1).czySciana != true) { return true; }
            if (i == 8 && siatka.GetGridObject(xp + 1, zp + 1).czyPosprzatane == true && siatka.GetGridObject(xp + 1, zp + 1).czySciana != true) { return true; }
        }


        return false;
    }

    public bool CzyWszystkoWyczyszczono()
    {
        for (int i = 0; i < rozmiarSiatki; i++)
        {
            for (int j = 0; j < rozmiarSiatki; j++)
            {
                if (siatka.GetGridObject(j, i).czyPosprzatane == false)
                { return false; }
            }
        }
        return true;
    }

    //public bool SprawdzKolejnyRuchCzyTrasa2(int ruch)
    //{

    //    if (ruch == 1 && siatka.GetGridObject(px + 1, pz).liczbaPrzejsc < 13 && siatka.GetGridObject(px + 1, pz).czySciana != true) { return true; }
    //    if (ruch == 2 && siatka.GetGridObject(px, pz + 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px, pz + 1).czySciana != true) { return true; }
    //    if (ruch == 3 && siatka.GetGridObject(px - 1, pz).liczbaPrzejsc < 13 && siatka.GetGridObject(px - 1, pz).czySciana != true) { return true; }
    //    if (ruch == 4 && siatka.GetGridObject(px, pz - 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px, pz - 1).czySciana != true) { return true; }

    //    if (ruch == 5 && siatka.GetGridObject(px + 1, pz - 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px + 1, pz - 1).czySciana != true) { return true; }
    //    if (ruch == 6 && siatka.GetGridObject(px - 1, pz - 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px - 1, pz - 1).czySciana != true) { return true; }
    //    if (ruch == 7 && siatka.GetGridObject(px - 1, pz + 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px - 1, pz + 1).czySciana != true) { return true; }
    //    if (ruch == 8 && siatka.GetGridObject(px + 1, pz + 1).liczbaPrzejsc < 13 && siatka.GetGridObject(px + 1, pz + 1).czySciana != true) { return true; }


    //    return false;
    //}

    //public void Najblizszy_Bialy()
    //{

    //    List<NP_List> nP_list = new List<NP_List>();
    //    int granica = rozmiarSiatki - 1;
    //    int Dopx = px + obszar;
    //    int Dopy = pz + obszar;
    //    int Uopx = px - obszar;
    //    int Uopy = pz - obszar;

    //    if (Dopx > granica)
    //    {
    //        Dopx = granica - 1;
    //    }
    //    if (Dopy > granica)
    //    {
    //        Dopy = granica - 1;
    //    }
    //    if (Uopx < 0)
    //    {
    //        Uopx = 0;
    //    }
    //    if (Uopy < 0)
    //    {
    //        Uopy = 0;
    //    }

    //    //xz

    //    for (int zz = pz; zz < Dopy; zz++)
    //    {
    //        if (Dopx > 0 && Dopx < granica)
    //        {
    //            for (int xx = px; xx <= Dopx; xx++)
    //            {
    //                int kara = 0;
    //                if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
    //                {
    //                    nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) + kara });
    //                }
    //            }

    //        }

    //    }


    //    //-x-z

    //    for (int zz = pz; zz > Uopy; zz--)
    //    {
    //        if (Uopx > 0 && Uopx < granica)
    //        {
    //            for (int xx = px; xx >= Uopx; xx--)
    //            {
    //                int kara = 0;

    //                if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
    //                {
    //                    nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) + kara });
    //                }

    //            }

    //        }

    //    }


    //    //x-z

    //    for (int zz = pz; zz > Uopy; zz--)
    //    {
    //        if (Dopx > 0 && Dopx < granica)
    //        {
    //            for (int xx = px; xx <= Dopx; xx++)
    //            {
    //                if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
    //                {
    //                    nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) });
    //                }
    //            }

    //        }

    //    }

    //    //-xz

    //    for (int zz = pz; zz < Dopy; zz++)
    //    {
    //        if (Uopx > 0 && Uopx < granica)
    //        {
    //            for (int xx = px; xx >= Uopx; xx--)
    //            {
    //                if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
    //                {
    //                    nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) });
    //                }
    //            }

    //        }

    //    }

    //    if (nP_list.Count < 5)
    //    {
    //        if (CzyWszystkoWyczyszczono() == true)
    //        {
    //            KoniecSprzatania = true;
    //            return;
    //        }
    //        if (obszar < rozmiarSiatki - 1)
    //        {
    //            obszar++;
    //            Najblizszy_Bialy();
    //        }
    //    }
    //    else
    //    {
    //        nP_list = nP_list.OrderBy(x => x.Distance)
    //        .ToList();
    //        Sciezka(nP_list[0].Coord_Y, nP_list[0].Coord_X);
    //    }
    //    obszar = 1;
    //}
    //public void Sciezka(int PY, int PX)
    //{
    //    obszar = 2;
    //    List<N_List> n_list = new List<N_List>();
    //    int P0Y = PY;
    //    int P0X = PX;


    //    int P1G = Math.Abs((P0X + 1) - P0X) + Math.Abs(P0Y - P0Y);
    //    int P2G = Math.Abs(P0X - P0X) + Math.Abs((P0Y + 1) - P0Y);
    //    int P3G = Math.Abs((P0X - 1) - P0X) + Math.Abs(P0Y - P0Y);
    //    int P4G = Math.Abs(P0X - P0X) + Math.Abs((P0Y - 1) - P0Y);

    //    int P5G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y - 1) - P0Y);
    //    int P6G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y - 1) - P0Y);
    //    int P7G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y + 1) - P0Y);
    //    int P8G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y + 1) - P0Y);



    //    int P1H = Math.Abs((P0X + 1) - px) + Math.Abs(P0Y - pz);
    //    int P2H = Math.Abs(P0X - px) + Math.Abs((P0Y + 1) - pz);
    //    int P3H = Math.Abs((P0X - 1) - px) + Math.Abs(P0Y - pz);
    //    int P4H = Math.Abs(P0X - px) + Math.Abs((P0Y - 1) - pz);

    //    int P5H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y - 1) - pz);
    //    int P6H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y - 1) - pz);
    //    int P7H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y + 1) - pz);
    //    int P8H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y + 1) - pz);

    //    int P1F = P1G + P1H;
    //    int P2F = P2G + P2H;
    //    int P3F = P3G + P3H;
    //    int P4F = P4G + P4H;

    //    int P5F = P5G + P5H;
    //    int P6F = P6G + P6H;
    //    int P7F = P7G + P7H;
    //    int P8F = P8G + P8H;

    //    if (SprawdzKolejnyRuchCzySciana(3) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(3) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 1, G = Math.Abs((P0X + 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs(P0Y - pz), F = P1G + P1H, R = 3 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 1, G = Math.Abs((P0X + 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs(P0Y - pz), F = P1G + P1H, R = 3 });
    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(3) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(3) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 1, G = Math.Abs((P0X + 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs(P0Y - pz), F = P1G + P1H + 1000, R = 3 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 1, G = Math.Abs((P0X + 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs(P0Y - pz), F = P1G + P1H + 1000, R = 3 });
    //            B++;
    //        }


    //    }

    //    if (SprawdzKolejnyRuchCzySciana(4) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(4) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 2, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y + 1) - pz), F = P2G + P2H, R = 4 });


    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 2, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y + 1) - pz), F = P2G + P2H, R = 4 });

    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(4) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(4) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 2, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y + 1) - pz), F = P2G + P2H + 1000, R = 4 });


    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 2, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y + 1) - pz), F = P2G + P2H + 1000, R = 4 });

    //            B++;
    //        }


    //    }

    //    if (SprawdzKolejnyRuchCzySciana(1) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(1) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 3, G = Math.Abs((P0X - 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs(P0Y - pz), F = P3G + P3H, R = 1 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 3, G = Math.Abs((P0X - 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs(P0Y - pz), F = P3G + P3H, R = 1 });


    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(1) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(1) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 3, G = Math.Abs((P0X - 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs(P0Y - pz), F = P3G + P3H + 1000, R = 1 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 3, G = Math.Abs((P0X - 1) - P0X) + Math.Abs(P0Y - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs(P0Y - pz), F = P3G + P3H + 1000, R = 1 });


    //            B++;
    //        }


    //    }

    //    if (SprawdzKolejnyRuchCzySciana(2) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(2) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 4, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y - 1) - pz), F = P4G + P4H, R = 2 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 4, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y - 1) - pz), F = P4G + P4H, R = 2 });
    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(2) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(2) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 4, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y - 1) - pz), F = P4G + P4H + 1000, R = 2 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 4, G = Math.Abs(P0X - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs(P0X - px) + Math.Abs((P0Y - 1) - pz), F = P4G + P4H + 1000, R = 2 });
    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(7) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(7) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 5, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y - 1) - pz), F = P5G + P5H - 1, R = 7 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 5, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y - 1) - pz), F = P5G + P5H - 1, R = 7 });
    //            B++;
    //        }



    //    }
    //    if (SprawdzKolejnyRuchCzySciana(7) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(7) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 5, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y - 1) - pz), F = P5G + P5H + 1000 - 1, R = 7 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 5, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y - 1) - pz), F = P5G + P5H + 1000 - 1, R = 7 });
    //            B++;
    //        }



    //    }
    //    if (SprawdzKolejnyRuchCzySciana(8) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(8) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 6, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y - 1) - pz), F = P6G + P6H - 1, R = 8 });
    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 6, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y - 1) - pz), F = P6G + P6H - 1, R = 8 });
    //            B++;
    //        }


    //    }

    //    if (SprawdzKolejnyRuchCzySciana(8) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(8) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 6, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y - 1) - pz), F = P6G + P6H + 1000 - 1, R = 8 });
    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 6, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y - 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y - 1) - pz), F = P6G + P6H + 1000 - 1, R = 8 });
    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(5) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(5) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 7, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y + 1) - pz), F = P7G + P7H - 1, R = 5 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 7, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y + 1) - pz), F = P7G + P7H - 1, R = 5 });
    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(5) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(5) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 7, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y + 1) - pz), F = P7G + P7H + 1000 - 1, R = 5 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 7, G = Math.Abs((P0X - 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X - 1) - px) + Math.Abs((P0Y + 1) - pz), F = P7G + P7H + 1000 - 1, R = 5 });
    //            B++;
    //        }


    //    }

    //    if (SprawdzKolejnyRuchCzySciana(6) == false)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(6) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 8, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y + 1) - pz), F = P8G + P8H - 1, R = 6 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 8, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y + 1) - pz), F = P8G + P8H - 1, R = 6 });

    //            B++;
    //        }


    //    }
    //    if (SprawdzKolejnyRuchCzySciana(6) == true)
    //    {
    //        if (SprawdzKolejnyRuchCzyTrasa2(6) == true && B > 1)
    //        {
    //            n_list.Add(new N_List() { PID = 8, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y + 1) - pz), F = P8G + P8H + 1000 - 1, R = 6 });

    //        }
    //        if (B == 1)
    //        {
    //            n_list.Add(new N_List() { PID = 8, G = Math.Abs((P0X + 1) - P0X) + Math.Abs((P0Y + 1) - P0Y), H = Math.Abs((P0X + 1) - px) + Math.Abs((P0Y + 1) - pz), F = P8G + P8H + 1000 - 1, R = 6 });

    //            B++;
    //        }


    //    }

    //    n_list = n_list.OrderBy(x => x.F)
    //   .ToList();
    //    int i = 0;
    //    Debug.Log(n_list.Count);
    //    if (n_list.Count > 1)
    //    {

    //        if (n_list[i].F == n_list[i + 1].F)
    //        {

    //            if (n_list[i].R < n_list[i + 1].R)
    //            {
    //                Kierunek(n_list[i].R);
    //                ruch = n_list[i].R;

    //            }
    //            else
    //            {
    //                Kierunek(n_list[i + 1].R);
    //                ruch = n_list[i + 1].R;
    //            }

    //        }
    //        else
    //        {
    //            Kierunek(n_list[i].R);
    //            ruch = n_list[i].R;
    //        }
    //    }
    //    else
    //    {
    //        Kierunek(n_list[i].R);
    //        ruch = n_list[i].R;
    //    }


    //    obszar = 1;
    //}
    public void Losowe_odbicia()
    {
        //RUCH PRAWO

        int r = 0;

        if (ruch == 1)
        {
            if (SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                Kierunek(1);
                ruch = 1;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true)
            {
                var arr1 = new[] { 2, 7 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true)
            {
                var arr1 = new[] { 6, 4 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzyPuste(2) == true
                     && SprawdzKolejnyRuchCzyPuste(6) == true && SprawdzKolejnyRuchCzyPuste(7) == true)
            {
                var arr1 = new[] { 4, 2, 6, 7 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }

        // RUCh LEWO
        if (ruch == 3)
        {
            if (SprawdzKolejnyRuchCzyPuste(3) == true)
            {
                Kierunek(3);
                ruch = 3;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true)
            {
                var arr1 = new[] { 2, 8 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true)
            {
                var arr1 = new[] { 4, 5 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzyPuste(2) == true && SprawdzKolejnyRuchCzyPuste(5) == true && SprawdzKolejnyRuchCzyPuste(8) == true)
            {
                var arr1 = new[] { 4, 5, 8, 2 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }

        // RUCh GORA


        if (ruch == 4)
        {
            if (SprawdzKolejnyRuchCzyPuste(4) == true)
            {
                Kierunek(4);
                ruch = 4;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true)
            {
                var arr1 = new[] { 1, 8 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true)
            {
                var arr1 = new[] { 3, 7 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzyPuste(3) == true && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzyPuste(7) == true && SprawdzKolejnyRuchCzyPuste(8) == true)
            {
                var arr1 = new[] { 1, 8, 7, 3 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }
        // RUCh DOL

        if (ruch == 2)
        {
            if (SprawdzKolejnyRuchCzyPuste(2) == true)
            {
                Kierunek(2);
                ruch = 2;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true)
            {
                var arr1 = new[] { 1, 5 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true)
            {
                var arr1 = new[] { 3, 6 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzyPuste(3) == true && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzyPuste(5) == true && SprawdzKolejnyRuchCzyPuste(6) == true)
            {
                var arr1 = new[] { 3, 6, 5, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }

        // RUCH 5

        if (ruch == 5)
        {
            if (SprawdzKolejnyRuchCzyPuste(5) == true)
            {
                Kierunek(5);
                ruch = 5;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                var arr1 = new[] { 1, 8, 2, 3 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;

            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true && SprawdzKolejnyRuchCzyPuste(4) == true)
            {
                var arr1 = new[] { 4, 6, 3, 2 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true && SprawdzKolejnyRuchCzySciana(4) == true)
            {
                var arr1 = new[] { 3, 2 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                var arr1 = new[] { 4, 6, 3, 2, 8, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }


        //RUCH 6

        if (ruch == 6)
        {
            if (SprawdzKolejnyRuchCzyPuste(6) == true)
            {
                Kierunek(6);
                ruch = 6;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzyPuste(3) == true)
            {
                var arr1 = new[] { 3, 7, 2, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;

            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true && SprawdzKolejnyRuchCzyPuste(4) == true)
            {
                var arr1 = new[] { 4, 5, 1, 2 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true && SprawdzKolejnyRuchCzySciana(4) == true)
            {
                var arr1 = new[] { 2, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzyPuste(3) == true)
            {
                var arr1 = new[] { 4, 5, 1, 2, 7, 3 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }
        //RUCH 7

        if (ruch == 7)
        {
            if (SprawdzKolejnyRuchCzyPuste(7) == true)
            {
                Kierunek(7);
                ruch = 7;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true && SprawdzKolejnyRuchCzyPuste(2) == true)
            {
                var arr1 = new[] { 2, 8, 1, 4 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;

            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzyPuste(3) == true)
            {
                var arr1 = new[] { 3, 6, 4, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true && SprawdzKolejnyRuchCzySciana(2) == true)
            {
                var arr1 = new[] { 4, 1 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(3) == true && SprawdzKolejnyRuchCzyPuste(2) == true)
            {
                var arr1 = new[] { 2, 8, 1, 4, 6, 3 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }
        //RUCH 8

        if (ruch == 8)
        {
            if (SprawdzKolejnyRuchCzyPuste(8) == true)
            {
                Kierunek(8);
                ruch = 8;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true && SprawdzKolejnyRuchCzyPuste(2) == true)
            {
                var arr1 = new[] { 2, 7, 3, 4 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;

            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                var arr1 = new[] { 1, 5, 4, 3 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;


            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true && SprawdzKolejnyRuchCzySciana(2) == true)
            {
                var arr1 = new[] { 3, 4 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
            else if (SprawdzKolejnyRuchCzyPuste(2) == true && SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                var arr1 = new[] { 1, 5, 4, 3, 7, 2 };
                r = arr1[random.Next(arr1.Length)];
                Kierunek(r);
                ruch = r;
                return;
            }
        }

    }
    public void PodazanieZaSciana()
    {
        if (Inicjacja == true && ruch == 1 && SprawdzKolejnyRuchCzyPuste(1) == true)
        {
            Kierunek(1);
            ruch = 1;
            return;
        }
        if (Inicjacja == true && ruch == 1 && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(4);
            ruch = 4;
            Inicjacja = false;
            return;
        }
        if (ruch == 4)
        {
            if (SprawdzKolejnyRuchCzyPuste(4) == true)
            {
                if (SprawdzKolejnyRuchCzySciana(1) == true)
                {
                    Kierunek(4);
                    ruch = 4;
                    return;
                }
                else
                {
                    Kierunek(1);
                    ruch = 1;
                    return;
                }
            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true)
            {
                Kierunek(3);
                ruch = 3;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true)
            {
                Kierunek(1);
                ruch = 1;
                return;
            }

        }
        if (ruch == 3)
        {
            if (SprawdzKolejnyRuchCzyPuste(3) == true)
            {
                if (SprawdzKolejnyRuchCzySciana(4) == true)
                {
                    Kierunek(3);
                    ruch = 3;
                    return;
                }
                else
                {
                    Kierunek(4);
                    ruch = 4;
                    return;
                }

            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true)
            {
                Kierunek(2);
                ruch = 2;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true)
            {
                Kierunek(4);
                ruch = 4;
                return;
            }

        }
        if (ruch == 2)
        {
            if (SprawdzKolejnyRuchCzyPuste(2) == true)
            {
                if (SprawdzKolejnyRuchCzySciana(3) == true)
                {
                    Kierunek(2);
                    ruch = 2;
                    return;
                }
                else
                {
                    Kierunek(3);
                    ruch = 3;
                    return;
                }

            }
            else if (SprawdzKolejnyRuchCzySciana(3) == true)
            {
                Kierunek(1);
                ruch = 1;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(1) == true)
            {
                Kierunek(3);
                ruch = 3;
                return;
            }

        }
        if (ruch == 1)
        {
            if (SprawdzKolejnyRuchCzyPuste(1) == true)
            {
                if (SprawdzKolejnyRuchCzySciana(2) == true)
                {
                    Kierunek(1);
                    ruch = 1;
                    return;
                }
                else
                {
                    Kierunek(2);
                    ruch = 2;
                    return;
                }

            }
            else if (SprawdzKolejnyRuchCzySciana(2) == true)
            {
                Kierunek(4);
                ruch = 4;
                return;
            }
            else if (SprawdzKolejnyRuchCzySciana(4) == true)
            {
                Kierunek(2);
                ruch = 2;
                return;
            }

        }


    }
    public void NavMeshAlg()
    {
        List<NP_List> nP_list = new List<NP_List>();
        int granica = rozmiarSiatki - 1;
        int Dopx = px + obszar;
        int Dopy = pz + obszar;
        int Uopx = px - obszar;
        int Uopy = pz - obszar;
        if (Dopx > granica)
        {
            Dopx = granica - 1;
        }
        if (Dopy > granica)
        {
            Dopy = granica - 1;
        }
        if (Uopx < 0)
        {
            Uopx = 0;
        }
        if (Uopy < 0)
        {
            Uopy = 0;
        }
        //xz
        for (int zz = pz; zz < Dopy; zz++)
        {
            if (Dopx > 0 && Dopx < granica)
            {
                for (int xx = px; xx <= Dopx; xx++)
                {
                    int kara = 0;
                    if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
                    {
                        nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) + kara });
                    }
                }
            }

        }


        //-x-z

        for (int zz = pz; zz > Uopy; zz--)
        {
            if (Uopx > 0 && Uopx < granica)
            {
                for (int xx = px; xx >= Uopx; xx--)
                {
                    int kara = 0;

                    if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
                    {
                        nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) + kara });
                    }

                }

            }

        }


        //x-z

        for (int zz = pz; zz > Uopy; zz--)
        {
            if (Dopx > 0 && Dopx < granica)
            {
                for (int xx = px; xx <= Dopx; xx++)
                {
                    if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
                    {
                        nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) });
                    }
                }

            }

        }

        //-xz

        for (int zz = pz; zz < Dopy; zz++)
        {
            if (Uopx > 0 && Uopx < granica)
            {
                for (int xx = px; xx >= Uopx; xx--)
                {
                    if (siatka.GetGridObject(xx, zz).czyPuste == true && SprawdzCzyPunktGraniczyZTrasa(xx, zz) == true)
                    {
                        nP_list.Add(new NP_List() { Coord_Y = zz, Coord_X = xx, Distance = Math.Abs(px - xx) + Math.Abs(pz - zz) });
                    }
                }

            }

        }

        if (nP_list.Count == 0)
        {
            if (CzyWszystkoWyczyszczono() == true)
            {
                KoniecSprzatania = true;
                return;
            }
            if (obszar < rozmiarSiatki - 1)
            {
                obszar++;
                NavMeshAlg();
            }
        }
        else
        {
            nP_list = nP_list.OrderBy(x => x.Distance)
            .ToList();

            pz = nP_list[0].Coord_Y;
            px = nP_list[0].Coord_X;
        }
        obszar = 1;
    }
    public void Ruch_S()
    {
        //RUCH prawo INICJACJA

        if (Inicjacja == true && ruch == 1 && SprawdzKolejnyRuchCzyPuste(1) == true)
        {
            Kierunek(1);
            ruch = 1;
            RPrawo = true;
            return;
        }
        if (Inicjacja == true && ruch == 1 && SprawdzKolejnyRuchCzyPuste(1) == false && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(4);
            ruch = 4;
            Inicjacja = false;
            return;
        }


        if (ruch == 4 && SprawdzKolejnyRuchCzyPuste(4) == true)
        {
            Kierunek(4);
            ruch = 4;
            return;
        }

        if (ruch == 2 && SprawdzKolejnyRuchCzyPuste(2) == true)
        {
            Kierunek(2);
            ruch = 2;
            return;
        }

        //PRAWY DOLNNY ROG

        if (ruch == 4 && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(3);
            ruch = 3;
            Krok1 = true;
            RPrawo = false;
            RLewo = true;
            return;
        }
        //PRAWY GORNY ROG
        if (ruch == 2 && SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(3);
            ruch = 3;
            Krok2 = true;
            RPrawo = false;
            RLewo = true;
            return;
        }
        //PRAWY DOLNY RÓG RUCH W PRAWY 
        if (ruch == 1 && SprawdzKolejnyRuchCzyPuste(2) == true && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(2);
            ruch = 2;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //PRAWY DOLNY RÓG RUCH W PRAWY
        if (ruch == 1 && SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzySciana(1) == true)
        {
            Kierunek(4);
            ruch = 4;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //KROK W LEWO DOLNY BEZ SCIANY
        if (RLewo == true && ruch == 4 && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzyPuste(3) == true && SprawdzKolejnyRuchCzyPuste(1) == true && Krok1 == false)
        {
            Kierunek(3);
            ruch = 3;
            Krok1 = true;
            return;
        }
        //KROK W LEWO GORNY
        if (RLewo == true && ruch == 3 && SprawdzKolejnyRuchCzyPuste(2) == true && Krok1 == true)
        {
            Kierunek(2);
            ruch = 2;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //

        //KROK W LEWO GORNY BEZ SCIANY
        if (RLewo == true && ruch == 2 && SprawdzKolejnyRuchCzyPuste(2) == false && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzyPuste(3) == true && Krok2 == false)
        {
            Kierunek(3);
            ruch = 3;
            Krok2 = true;
            return;
        }
        //KROK W LEWO DOLNY
        if (RLewo == true && ruch == 3 && SprawdzKolejnyRuchCzyPuste(4) == true && Krok2 == true)
        {
            Kierunek(4);
            ruch = 4;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //LEWY GORNY RÓG RUCH W LEWO 
        if (ruch == 3 && SprawdzKolejnyRuchCzyPuste(2) == true && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzySciana(3) == true)
        {
            Kierunek(2);
            ruch = 2;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //LEWY DOLNY RÓG RUCH W LEWO
        if (ruch == 3 && SprawdzKolejnyRuchCzyPuste(4) == true && SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzySciana(3) == true)
        {
            Kierunek(4);
            ruch = 4;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //LEWY GORNY RÓG RUCH W GORE 
        if (ruch == 4 && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzySciana(3) == true)
        {
            Kierunek(1);
            ruch = 1;
            RLewo = false;
            RPrawo = true;
            Krok3 = true;
            return;
        }

        //LEWY DOLNY RÓG RUCH W DOL
        if (ruch == 2 && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzySciana(2) == true && SprawdzKolejnyRuchCzySciana(3) == true)
        {
            Kierunek(1);
            ruch = 1;
            RLewo = false;
            RPrawo = true;
            Krok4 = true;
            return;
        }

        //KROK W PRAWO GORNY BEZ SCIANY
        if (ruch == 4 && SprawdzKolejnyRuchCzySciana(4) == true && SprawdzKolejnyRuchCzyPuste(3) == true && SprawdzKolejnyRuchCzyPuste(1) == true && Krok3 == false)
        {
            Kierunek(1);
            ruch = 1;
            Krok3 = true;
            return;
        }
        //KROK W PRAWO GORNY
        if (RPrawo == true && ruch == 1 && SprawdzKolejnyRuchCzyPuste(2) == true && Krok3 == true)
        {
            Kierunek(2);
            ruch = 2;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }
        //

        //KROK W PRAWO DOLNY BEZ SCIANY
        if (RPrawo == true && ruch == 2 && SprawdzKolejnyRuchCzyPuste(2) == false && SprawdzKolejnyRuchCzyPuste(1) == true && SprawdzKolejnyRuchCzyPuste(3) == true && Krok4 == false)
        {
            Kierunek(1);
            ruch = 1;
            Krok4 = true;
            return;
        }
        //KROK W PRAWO DOLNY
        if (RPrawo == true && ruch == 1 && SprawdzKolejnyRuchCzyPuste(4) == true && Krok4 == true)
        {
            Kierunek(4);
            ruch = 4;
            Krok1 = false;
            Krok2 = false;
            Krok3 = false;
            Krok4 = false;
            return;
        }




    }
    public void ZPamieciaTrasy()
    {
        int r = 0;
        List<int> P_list = new List<int>();
        for (int i = 1; i <= 8; i++)
        {
            if (SprawdzKolejnyRuchCzyPuste(i) == true && SprawdzKolejnyRuchCzySciana(i) == false && SprawdzKolejnyRuchCzyTrasa(i) == false)
            {
                P_list.Add(i);
            }
        }
        var arr1 = P_list.ToArray();
        if (arr1.Length == 0)
        {
            Losowe_odbicia();
        }
        else
        {
            Array.Sort(arr1);
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == 1)
                {
                    r = arr1[i];
                    break;
                }
                else
                {
                    if (arr1[i] == 2)
                    {
                        r = arr1[i];
                        break;
                    }
                    else
                    {
                        if (arr1[i] == 3)
                        {
                            r = arr1[i];
                            break;
                        }
                        else
                        {
                            if (arr1[i] == 4)
                            {
                                r = arr1[i];
                                break;
                            }
                            else
                            {
                                if (arr1[i] == 5)
                                {
                                    r = arr1[i];
                                    break;
                                }
                                else
                                {
                                    if (arr1[i] == 6)
                                    {
                                        r = arr1[i];
                                        break;
                                    }
                                    else
                                    {
                                        if (arr1[i] == 7)
                                        {
                                            r = arr1[i];
                                            break;
                                        }
                                        else
                                        {
                                            if (arr1[i] == 8)
                                            {
                                                r = arr1[i];
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Kierunek(r);
            ruch = r;
        }

    }
    private void DodajSciany()
    {

        for (int x = 0; x < 50; x++)
        {
            int z = 0;


            siatka.GetGridObject(x, z).czySciana = true;
            siatka.TriggerGridObjectChanged(x, z);



        }
        for (int x = 0; x < 50; x++)
        {
            int z = 49;


            siatka.GetGridObject(x, z).czySciana = true;
            siatka.TriggerGridObjectChanged(x, z);



        }

        for (int z = 0; z < 50; z++)
        {
            int x = 49;
            siatka.GetGridObject(x, z).czySciana = true;
            siatka.TriggerGridObjectChanged(x, z);

        }
        for (int z = 0; z < 50; z++)
        {
            int x = 0;
            siatka.GetGridObject(x, z).czySciana = true;
            siatka.TriggerGridObjectChanged(x, z);

        }



    }
    public void RysujMape()
    {
        Mapy mapy = new Mapy();
        mapy.PlanszaG = DropDownItemSelected(WyborPlanszy) switch
        {
            0 => mapy.Plansza,
            1 => mapy.Plansza1,
            2 => mapy.Plansza2,
            3 => mapy.Plansza3,
            4 => mapy.Plansza4,
            _ => mapy.Plansza1,
        };
        for (int pz = 0; pz < 40; pz++)
        {
            for (int px = 0; px < 40; px++)
            {
                if (mapy.PlanszaG[pz, px] == 99)
                {
                    Instantiate(Sciany, siatka.GetWorldPosition(px, pz), Quaternion.identity);
                    siatka.GetGridObject(px, pz).czySciana = true;
                    siatka.GetGridObject(px, pz).czyPosprzatane = true;
                    siatka.GetGridObject(px, pz).czyPuste = false;
                    siatka.GetGridObject(px, pz).liczbaPrzejsc = 40;
                    siatka.TriggerGridObjectChanged(px, pz);
                }
            }

        }
        surfance.BuildNavMesh();
    }
    int DropDownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        return index;
    }
    private bool CzyWysprz¹tane()
    {

        for (int x = 0; x < siatka.GetWidth(); x++)
        {
            for (int z = 0; z < siatka.GetHeight(); z++)
            {
                if (siatka.GetGridObject(x, z).czyPosprzatane == false)
                {
                    return false;
                }

            }
        }
        return true;
    }

    public void RozpocznijSymulacje()
    {

        start = true;

    }
    public void ZatrzymajSymulacje()
    {
        start = false;
    }
    public void ResetSceny()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ResetujSymulacje()
    {
        start = false;
        for (int x = 0; x < siatka.GetWidth(); x++)
        {
            for (int z = 0; z < siatka.GetHeight(); z++)
            {
                if (siatka.GetGridObject(x, z).czyPosprzatane == true)
                {
                    siatka.GetGridObject(x, z).czyPosprzatane = false;
                    siatka.GetGridObject(x, z).czyPuste = false;
                    siatka.GetGridObject(x, z).liczbaPrzejsc = 10;
                    siatka.TriggerGridObjectChanged(x, z);
                }

            }
        }

        odkurzacz.transform.position = pozStart;
        CelOdkurzacza.transform.position = pozStart;
        pz = (int)pozStart.z;
        px = (int)pozStart.x;
        polaWyczyszczone.text = "Pola wyczyszczone: ";
        polaDoWyczyszczenia.text = "Pola do wyczyszczenia: ";
    }
    private void SprawdzmapeCzyPosprzatana()
    {
        for (int x = 0; x < siatka.GetWidth(); x++)
        {
            for (int z = 0; z < siatka.GetHeight(); z++)
            {
                if (siatka.GetGridObject(x, z).czyPosprzatane == false)
                {
                    KoniecSprzatania = false;
                    return;
                }

            }
        }
        KoniecSprzatania = true;

    }
    public int PolaWyczyszczone()
    {
        int polaPosprzatane = 0;
        for (int x = 0; x < siatka.GetWidth(); x++)
        {
            for (int z = 0; z < siatka.GetHeight(); z++)
            {
                if (siatka.GetGridObject(x, z).czyPosprzatane == true && siatka.GetGridObject(x, z).czySciana == false)
                {
                    polaPosprzatane++;
                }
            }
        }
        return polaPosprzatane;
    }
    public int WszystkeSciany()
    {
        int sciany = 0;
        for (int x = 0; x < siatka.GetWidth(); x++)
        {
            for (int z = 0; z < siatka.GetHeight(); z++)
            {
                if (siatka.GetGridObject(x, z).czySciana == true)
                {
                    sciany++;
                }
            }
        }
        return sciany;
    }
    public int WszystkiePola()
    {
        int wszystkiePola = siatka.GetWidth() * siatka.GetHeight() - WszystkeSciany();
        return wszystkiePola;
    }
    public int PolaDoWyczyszczenia()
    {
        int polaDowyczyszczenia = WszystkiePola() - PolaWyczyszczone();
        return polaDowyczyszczenia;
    }
    public float ProcentWyczyszczenia()
    {
        float pdw = PolaWyczyszczone();
        float wp = WszystkiePola(); 
        float ProcentCzystosci;
        ProcentCzystosci = pdw / wp * 100;
        return ProcentCzystosci;
    }
    private void Awake()
    {

        float cellSize = 1f;
        ruch = 1;
        siatka = new SiatkaXZ<SiatkaObiektow>(rozmiarSiatki, rozmiarSiatki, cellSize, Vector3.zero, (SiatkaXZ<SiatkaObiektow> g, int x, int z) => new SiatkaObiektow(g, x, z), Tekst);

        px = (int)CelOdkurzacza.transform.position.x;
        pz = (int)CelOdkurzacza.transform.position.z;
        CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
        siatka.GetGridObject(px, pz).czyPosprzatane = true;
        siatka.TriggerGridObjectChanged(px, pz);
        timerIsRunning = true;
        pozStart = odkurzacz.transform.position;

    }



    public void FixedUpdate()
    {

        //Komunikacja z targetem
        PlayerControler sc = partner.GetComponent<PlayerControler>();
        CzyKolizja = sc.f;
        if (ProcentWyczyszczenia() >=100)
        {
            KoniecSprzatania = true;
            przycisk.onClick.Invoke();
        }

        //Wyznaczanie pozycji targetu
        if (start && KoniecSprzatania == false)
        {
            if (CzyKolizja == true)
            {
                if (timerIsRunning)
                {
                    if (czasTargetu > 0)
                    {
                        czasTargetu -= Time.deltaTime;
                    }
                    else
                    {
                        SprawdzmapeCzyPosprzatana();
                        switch (DropDownItemSelected(WyborAlgorytmu))
                        {
                            case 0:
                                PodazanieZaSciana();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Pod¹¿anie wzd³óŸ sciany";
                                break;
                            case 1:
                                NavMeshAlg();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Z u¿yciem NavMesh";
                                break;
                            case 2:
                                Ruch_S();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Ruch S";
                                break;
                            case 3:
                                ZPamieciaTrasy();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Z pamiêci¹ trasy";
                                break;
                            case 4:
                                Losowe_odbicia();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Losowe odbicia";
                                break;
                            default:
                                Losowe_odbicia();
                                CelOdkurzacza.transform.position = siatka.GetWorldPosition(px, pz);
                                aktualnyAlgorytm.text = "Aktualny algorytm: Losowe odbicia";
                                break;
                        }

                        czasTargetu = 0.5f;
                        SiatkaObiektow gridObject = siatka.GetGridObject(px, pz);

                        if (siatka.GetGridObject((int)odkurzacz.transform.position.x, (int)odkurzacz.transform.position.z).liczbaPrzejsc > 11)
                        {
                            ponownePrzejsciaPola++;
                        }
                        gridObject.SetPosprzatanie();
                        siatka.GetGridObject(px, pz).liczbaPrzejsc++;
                        siatka.TriggerGridObjectChanged(px, pz);
                        polaWyczyszczone.text = "Pola wyczyszczone: " + PolaWyczyszczone();
                        polaDoWyczyszczenia.text = "Pola do wyczyszczenia: " + PolaDoWyczyszczenia();
                        polaWyczyszczoneProc.text = "Procent wyczyszczenia: " + ProcentWyczyszczenia().ToString("N3") + "%";
                        ponownePrzejscia.text = "Ponowne przejscia: " + ponownePrzejsciaPola;



                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
