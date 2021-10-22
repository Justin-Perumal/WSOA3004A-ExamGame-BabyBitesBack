using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    public bool ZoneOneEntered;
    public bool ZoneTwoEntered;
    public bool ZoneThreeEntered;
    // Start is called before the first frame update
    void Start()
    {
        ZoneOneEntered = false;
        ZoneTwoEntered = false;
        ZoneThreeEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player") && gameObject.tag == "Zone One")
        {
            ZoneOneEntered = true;
            ZoneTwoEntered = false;
            ZoneThreeEntered = false;
        }

        if(Col.CompareTag("Player") && gameObject.tag == "Zone Two")
        {
            ZoneOneEntered = false;
            ZoneTwoEntered = true;
            ZoneThreeEntered = false;
        }

        if(Col.CompareTag("Player") && gameObject.tag == "Zone Three")
        {
            ZoneOneEntered = false;
            ZoneTwoEntered = false;
            ZoneThreeEntered = true;
        }
    }
}
