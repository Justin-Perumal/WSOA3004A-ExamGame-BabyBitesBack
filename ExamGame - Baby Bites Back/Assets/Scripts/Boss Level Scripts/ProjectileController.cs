using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProjectileDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ProjectileDespawn()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
