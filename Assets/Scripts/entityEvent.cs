using UnityEngine;

public class entityAnimationEvent : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTarget()=>entity.Attack();
    private void DisableMovementAndJump() => entity.EnableMovementAndJump(false);
    private void EnableMovementAndJump() => entity.EnableMovementAndJump(true);

}
