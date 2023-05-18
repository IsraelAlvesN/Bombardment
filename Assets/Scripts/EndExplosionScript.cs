using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplosionScript : MonoBehaviour
{
    [SerializeField] private float delay = 2.5f;
    void Start()
    {
        StartCoroutine(BeginSelfDestruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BeginSelfDestruction()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
