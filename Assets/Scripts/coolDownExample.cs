using UnityEngine;

public class coolDownExample : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float redColorDuration=1;
    public float timer;

    public float currentTimeInGame;
    public float lastTimeWasDamage;
    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        currentTimeInGame = Time.time;

        if (currentTimeInGame > lastTimeWasDamage + redColorDuration)
        {
            if (sr.color != Color.white)
            {
                sr.color = Color.white;
            }
        }
    }

    public void TakeDamage()
    {
        // Debug.Log(gameObject.name +"Took Damage");

        sr.color=Color.red;
        // timer = -redColorDuration;
        lastTimeWasDamage = Time.time;

    }

    private void TurnWhite()
    {
        sr.color = Color.white;
    }
}
