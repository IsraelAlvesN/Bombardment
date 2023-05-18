using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 5f;
    [SerializeField] private GameObject explosionPrefab;

    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    void Update()
    {
        
    }

    private IEnumerator ExplosionCoroutine()
    {
        //wait
        yield return new WaitForSeconds(explosionDelay);
        //expldoe
        Explode();
    }
    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
