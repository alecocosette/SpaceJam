using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerAround : MonoBehaviour
{
    public Transform player;
    public int howMuchToRight;
    public int howMuchUp;
    void Update()
    {
        transform.position = new Vector3(player.position.x + howMuchToRight, player.position.y+howMuchUp, -10); // Camera follows the player but 6 to the right
    }

}
