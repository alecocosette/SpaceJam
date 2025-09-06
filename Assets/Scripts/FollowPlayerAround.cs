using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerAround : MonoBehaviour
{
    public Transform player;
    public int howMuchToRight;
    void Update()
    {
        transform.position = new Vector3(player.position.x + howMuchToRight, 0, -10); // Camera follows the player but 6 to the right
    }

}
