using UnityEngine;

public class attackEvent : MonoBehaviour
{
    private player player;

    private void Awake()
    {
        player = GetComponentInParent<player>();
    }

    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);
    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);

}
