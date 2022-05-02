using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float m_Speed;
    public Rigidbody m_Rigidbody;
    public GameObject target;
    private int total_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody.velocity = transform.forward * m_Speed;
        StartCoroutine(run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator run()
    {
        while (true)
        {
            total_time += 1;
            if (target != null) {
                transform.LookAt(target.transform);
            }
            m_Rigidbody.velocity += transform.forward * m_Speed;
            yield return new WaitForSeconds(1.0f);
            if (total_time > 3)
            {
                DestroyImmediate(this.gameObject, true);
                break;
            }
        }
    }


}
