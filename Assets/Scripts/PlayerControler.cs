using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public NavMeshAgent agent;
    public bool f;
    public Dropdown wyborPredkosci;
    public float RotationSpeed;

    private Quaternion _lookRotation;
    private Vector3 _direction;
    private Vector3 _position;
    private Vector3 _position2;
    private Vector3 destinationPoint;
    private bool srt = false;
    private float czasTargetu;
    private float czas1;

    public bool GetFF(bool f)
    {
        return f;
    }
    int DropDownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        return index;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            f = true;
        }
      
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            f = false;
        }
    }
    public void startbutton()
    {
        srt = true;
    }
    public void ZmienPrendkosc()
    {
        agent.acceleration = 2;
        agent.speed = 1f;

    }
    private void ZapiszCzas(float czas)
    {
        czas1 = czasTargetu;

    }
    // Update is called once per frame
    void Update()
    {
        if (srt == true)
        {
            _position = destinationPoint;
            destinationPoint = new Vector3(target.position.x, 0, target.position.z);
            agent.SetDestination(destinationPoint);
            _position2 = destinationPoint;

        }

        Vector3 _direction1 = (_position - transform.position).normalized;
        Vector3 _direction2 = (_position2 - transform.position).normalized;
        Quaternion _lookRotation1 = Quaternion.LookRotation(_direction1);
        Quaternion _lookRotation2 = Quaternion.LookRotation(_direction2);
        float roznica = Math.Abs(_lookRotation1.eulerAngles.magnitude) - Math.Abs(_lookRotation2.eulerAngles.magnitude);

        czasTargetu += Time.deltaTime;
        if (Math.Abs(roznica) > 40)
        {
            float czas = czasTargetu;
            agent.acceleration = 0.1f;
            agent.speed = 0.1f;
            ZapiszCzas(czas);
        }
        else if (czas1 + 3 < czasTargetu)
        {
            ZmienPrendkosc();
            czasTargetu = 0;
        }

    }
}
