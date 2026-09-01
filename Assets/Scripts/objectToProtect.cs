using UnityEngine;

public class objectToProtect : Entity
{
    [Header("Extra Details")]
    private Transform player;

    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<player>()?.transform;
    }
    protected override void Update()
    {
        HandleFlipping();
    }

    protected override void HandleFlipping()
    {
        if(player==null)
            return;

         if (player.transform.position.x>transform.position.x && facingRight==true)
        {
            Flip();
        }
        else if (player.transform.position.x<transform.position.x && facingRight==false)
        {
            Flip();
        }
    }
}
