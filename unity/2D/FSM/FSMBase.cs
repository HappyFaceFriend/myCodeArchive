using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBase : MonoBehaviour
{
    public enum State
    {
        Idle=0, Walk=1, Dash=2, Attack=3, Hitted=4, Die=5, Other=6
    }
    protected State currentState;
    protected bool isNewState;

    protected Rigidbody2D rigidbody;
    protected Knockback knockbackComponent;
    protected MovementController movementController;

    [SerializeField]
    protected Transform imageTransform;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;


    protected int maxHp;
    protected int currentHp;


    protected void Awake()
    {
        currentState = State.Idle;

        rigidbody = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MovementController>();
        knockbackComponent = GetComponent<Knockback>();
        animator = imageTransform.GetComponent<Animator>();
        spriteRenderer = imageTransform.GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        StartCoroutine(FSMMain());
    }
    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(currentState.ToString());
        }
    }
    public void SetState(State state)
    {
        currentState = state;
        isNewState = true;
        animator.SetInteger("State", (int)currentState);
    }
    protected void SetAnimTrigger(string triggerName)
    {
        animator.SetTrigger("triggerName");
    }

    protected void DisableComponent(MonoBehaviour component)
    {
        component.enabled = false;
    }
    protected void EnableComponent(MonoBehaviour component)
    {
        component.enabled = true;
    }
    protected void KnockBack(Vector2 direction, float distance, Knockback.Type type)
    {
        knockbackComponent.ApplyKnockback(direction, distance, type);
    }
    private void FixedUpdate()
    {
        
    }
}
