using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float distanceToStop = 300.0f;

    private CharacterScript player;

    private void Start()
    {
        player = FindObjectOfType<CharacterScript>();
    }

    private void Update()
    {
        if (player == null) 
        {
            Debug.Log("player is null!");
            return;
        }

        if (Vector3.Distance(transform.position, player.gameObject.transform.position) < distanceToStop)
        {
            return;
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
}
