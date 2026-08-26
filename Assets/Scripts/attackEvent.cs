using UnityEngine;

public class attackEvent : MonoBehaviour
{
    private player player;

    private void Awake()
    {
        player = GetComponentInParent<player>();
    }

    public void DamageEnemies()=>player.DamageEnemies();
    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);
    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);

}
