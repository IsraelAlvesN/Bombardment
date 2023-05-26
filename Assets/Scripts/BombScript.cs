using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 5f;
    [SerializeField] private GameObject explosionPrefab;
    public float blastRadius = 6f;
    public int blastDamage = 12;

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

        //destroy platforms
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider collider in colliders)
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if(lifeScript != null)
                {
                    //gameObject distance to explosion
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / blastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 6);
                    int damage = (int) Mathf.Ceil(damageRate * blastDamage);
                    lifeScript.health -= damage;

                    if (lifeScript.health <= 0)
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}
