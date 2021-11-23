using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackBehavour : MonoBehaviour
{
    public Vector3 SizeChange;
    // Start is called before the first frame update
    void Start()
    {
        SizeChange = new Vector3(0.01f,0.01f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale += SizeChange;
    }
}
