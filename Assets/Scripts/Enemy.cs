using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected String enemyName;
    
    void Awake()
    {
       sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
                Attack();
    }

    private void MoveAround()
    {
        // Debug.Log(enemyName+"moves at speed"+moveSpeed);
    }

    protected virtual void Attack()
    {
        // Debug.Log(enemyName+ " attacks");
        
    }

    public void TakeDamage()
    {
        // sr.color = Color.red;
    }

    public String getEnemyName()
    {
        return enemyName;
    }
}
