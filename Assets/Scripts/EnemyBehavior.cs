using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EnemyBehavior : TargetObject
{
    [SerializeField] private AttackType type = AttackType.NORMAL;
    [SerializeField] private float playerDetectRange;
    [SerializeField] private float wallDetectRange;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damageToPlayer;
    [SerializeField] private GameObject deathEffectObject;

    private SoundModule_Base sound;
    private PlayerBehavior targetPlayer;
    private Rigidbody rBody;
    [SerializeField]private NPCState state = NPCState.DISABLED;

    private float currentHealth;
    private float behaviorTimer;
    
    
    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        sound = GetComponent<SoundModule_Base>();
    }

    public override void Start()
    {
        base.Start();

        if (PlayerBehavior.Instance != null)
            targetPlayer = PlayerBehavior.Instance;

        currentHealth = maxHealth;
        StartCoroutine(BehaviorRoutine());
    }

    private void Update()
    {
        if(transform.position.y < -50f)
            Kill(new HitData());
    }

    public override void OnHit(HitData hitData)
    {
        if (state == NPCState.DISABLED) return;
        state = NPCState.PINNED;
        GetDamage(hitData);
        behaviorTimer = hitData._stiffTime;
    }

    public override void GameStarted()
    {
        state = NPCState.IDLE;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (state == NPCState.DISABLED) return;
        if (other.gameObject.tag == "Player" && state != NPCState.PINNED)
        {
            targetPlayer.GetDamage(damageToPlayer);
        }
    }


    IEnumerator BehaviorRoutine()
    {
        behaviorTimer = 2.0f;

        while (true)
        {
            switch (state)
            {
                case NPCState.DISABLED:
                    yield return new WaitForFixedUpdate();
                    break;
                
                case NPCState.IDLE:
                    if (PlayerDetected())
                    {
                        state = NPCState.CHASE;
                        behaviorTimer = 5.0f;
                        break;
                    }

                    behaviorTimer -= Time.deltaTime;
                    if (behaviorTimer <= 0)
                    {
                        if (Random.Range(0, 2) == 0)
                            behaviorTimer = Random.Range(0.5f, 5.0f);
                        else
                        {
                            headToRandomForward();
                            state = NPCState.ROAM;
                        }
                    }

                    yield return new WaitForFixedUpdate();
                    break;

                case NPCState.ROAM:
                    if (PlayerDetected())
                    {
                        state = NPCState.CHASE;
                        behaviorTimer = 5.0f;
                        break;
                    }

                    rBody.AddForce(transform.forward * speed, ForceMode.Force);
                    behaviorTimer -= Time.deltaTime;

                    if (behaviorTimer <= 0)
                    {
                        if (Random.Range(0, 1) == 0)
                        {
                            behaviorTimer = Random.Range(0.5f, 5.0f);
                            headToRandomForward();
                        }
                        else
                        {
                            state = NPCState.IDLE;
                        }
                    }

                    yield return new WaitForFixedUpdate();
                    break;

                case NPCState.CHASE:

                    Vector3 f = (targetPlayer.transform.position - transform.position).normalized;
                    transform.forward = new Vector3(f.x,0f,f.z);
                    rBody.AddForce(transform.forward * speed, ForceMode.Force);

                    behaviorTimer -= Time.deltaTime;

                    if (behaviorTimer < 0)
                    {
                        if (PlayerDetected())
                        {
                            behaviorTimer = 5.0f;
                        }
                    }

                    yield return new WaitForFixedUpdate();
                    break;

                case NPCState.PINNED:
                    rBody.velocity = Vector3.zero;
                    if (behaviorTimer < 0)
                    {
                        state = NPCState.IDLE;
                    }

                    behaviorTimer -= Time.deltaTime;
                    yield return new WaitForFixedUpdate();
                    break;
            }
        }
    }

    private bool PlayerDetected()
    {
        return Vector3.Distance(targetPlayer.transform.position, transform.position) < playerDetectRange;
    }

    private void headToRandomForward()
    {
        var dir_v2 = Random.insideUnitCircle.normalized;
        var dir = new Vector3(dir_v2.x, 0f, dir_v2.y);
        while (Physics.Raycast(transform.position, dir, wallDetectRange, 6))
        {
            dir_v2 = Random.insideUnitCircle.normalized;
            dir = new Vector3(dir_v2.x, 0f, dir_v2.y);
        }

        transform.rotation = quaternion.LookRotation(dir, Vector3.up);
    }

    public void GetDamage(HitData hit)
    {
        if ( hit._dealtDamage > currentHealth)
        {
            currentHealth = 0f;
            Kill(hit);
            return;
        }

        currentHealth -= hit._dealtDamage;

    }

    public void Kill(HitData hit)
    {
        state = NPCState.DISABLED;
        Instantiate(deathEffectObject, transform.position, quaternion.identity);
        rBody.constraints = RigidbodyConstraints.None;
        rBody.drag = 0f;
        rBody.AddForce((hit._direction + Vector3.up)*100f,ForceMode.Impulse);
        rBody.AddTorque(Random.insideUnitSphere * 50f);
        StartCoroutine(DelayedDestroy());

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * wallDetectRange);
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1f);
        StageManager.Instance.RemoveEnemyObject(gameObject);
        Destroy(gameObject);
    }
    
}