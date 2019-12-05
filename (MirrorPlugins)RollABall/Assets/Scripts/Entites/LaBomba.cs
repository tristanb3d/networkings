using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LaBomba : NetworkBehaviour
{

    public float explosionRadius = 5f;
    public float explosionDelay = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explod());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Explod()
    {
        yield return new WaitForSeconds(explosionDelay);
        CmdExplode(transform.position, explosionRadius);
        
    }

    [Command]
    void CmdExplode(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            NetworkServer.Destroy(hit.gameObject);
            Debug.Log("hits");
        }

    }

}
