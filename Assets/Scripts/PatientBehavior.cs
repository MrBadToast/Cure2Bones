using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public enum NPCState
{
    DISABLED,
    IDLE,
    ROAM,
    CHASE,
    PINNED
}

public class PatientBehavior : TargetObject
{
    [SerializeField] private AttackType type = AttackType.NORMAL;
    [SerializeField] private float playerDetectRange;
    [SerializeField] private float wallDetectRange;
    [SerializeField] private float speed;
    [SerializeField] private float hpToHeal;
    [SerializeField] private int moneyDrop;
    [SerializeField] private GameObject FinalHitEffectObject;
    [SerializeField] private GameObject[] GermObjects;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite curedSprite;

    private PlayerBehavior targetPlayer;
    private Rigidbody rBody;
    private NPCState state = NPCState.DISABLED;

    private float healProgress = 0f;
    private float behaviorTimer;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        base.Start();
        
        if (PlayerBehavior.Instance != null)
            targetPlayer = PlayerBehavior.Instance;
        
        StartCoroutine(BehaviorRoutine());
    }
    public override void OnHit(HitData hitData)
    {
        if (state == NPCState.DISABLED) return;
        state = NPCState.PINNED;
        behaviorTimer = hitData._stiffTime;
        GetHealed(hitData);
    }

    public override void GameStarted()
    {
        state = NPCState.IDLE;
    }

    IEnumerator BehaviorRoutine()
    {
        behaviorTimer = 2.0f;
        
        while (true)
        {
            switch (state)
            {
                case NPCState.DISABLED:
                    yield return null;
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
                            behaviorTimer = Random.Range(0.5f,5.0f);
                        else
                        {
                            headToRandomForward();
                            state = NPCState.ROAM;
                        }
                    }
                    yield return null;
                    break;
                
                case NPCState.ROAM:
                    if (PlayerDetected())
                    {
                        state = NPCState.CHASE;
                        behaviorTimer = 5.0f;
                        break;
                    }
                    
                    rBody.AddForce(transform.forward * speed,ForceMode.Force);
                    behaviorTimer -= Time.deltaTime;
                    
                    if (behaviorTimer <= 0)
                    {
                        if (Random.Range(0, 1) == 0)
                        {
                            behaviorTimer = Random.Range(0.5f,5.0f);
                            headToRandomForward();
                        }
                        else
                        {
                            state = NPCState.IDLE;
                        }
                    }
                    yield return null;
                    break;
                    
                case NPCState.CHASE:

                    transform.forward = (targetPlayer.transform.position - transform.position).normalized;
                    rBody.AddForce(transform.forward * speed,ForceMode.Force);
                    
                    behaviorTimer -= Time.deltaTime;
                    
                    if (behaviorTimer < 0)
                    {
                        if (PlayerDetected())
                        {
                            behaviorTimer = 5.0f;
                        }
                    }

                    yield return null;
                    break;
                
                case NPCState.PINNED:
                    rBody.velocity = Vector3.zero;
                    if (behaviorTimer < 0)
                    {
                        state = NPCState.IDLE;
                    }
                    behaviorTimer -= Time.deltaTime;
                    yield return null;
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

        transform.rotation = quaternion.LookRotation(dir,Vector3.up);
    }

    public void GetHealed(HitData hit)
    {
        if (hit._dealtDamage + healProgress >= hpToHeal)
        {
            HealComplete(hit);
            return;
        }
        healProgress += hit._dealtDamage;
    }
    
    public void HealComplete(HitData hit)
    {
        PlayerBehavior.Instance.GetMoney(moneyDrop);
        spriteRenderer.sprite = curedSprite;
        state = NPCState.DISABLED;
        
        rBody.AddForce(hit._direction * 50f, ForceMode.Impulse);
        
        foreach (var obj in GermObjects)
        {
            if (!obj) break;
            
            var germ = Instantiate(obj, transform.position, transform.rotation);
            Rigidbody germ_rb;
            if (germ.TryGetComponent(out germ_rb))
            {
                germ_rb.AddForce(
                    (hit._direction + Vector3.Cross(hit._direction,Vector3.up) * Random.Range(-1f,1f)) * 100f, ForceMode.Impulse);
            }
            germ.GetComponent<EnemyBehavior>().GameStarted();
            StageManager.Instance.RegisterEnemyObject(germ);
        }

        Instantiate(FinalHitEffectObject, transform.position, quaternion.identity);
        StartCoroutine(DelayedDestroy());

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,playerDetectRange);
        Gizmos.DrawLine(transform.position,transform.position + transform.forward * wallDetectRange);
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        StageManager.Instance.RemoveEnemyObject(gameObject);
        Destroy(gameObject);
    }
}