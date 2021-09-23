using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    Player player;
    float verticalAxis;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalAxis = Input.GetAxisRaw("Vertical");
        if (player.isGrounded)
        {
            player.anim.SetFloat("CrouchFLoat", verticalAxis);
            player.anim.SetBool("FallBool", player.isGrounded);
            if (Mathf.Approximately(verticalAxis,-1))
            {
                player.isCrouched = true;
            }
            else
            {
                player.isCrouched = false;
            }
        }
    }
}
