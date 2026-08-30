using UnityEngine;

public class attackEvent : MonoBehaviour
{
    private Entity player;

    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }

    public void DamageEnemies()=>player.DamageTarget();
    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);
    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);

}
