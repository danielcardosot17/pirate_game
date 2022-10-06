using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + Vector3.forward * -10;
    }
}
