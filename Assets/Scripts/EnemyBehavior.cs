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

    private PlayerBehavior targetPlayer;
    private Rigidbody rBody;
    private PatientState state = PatientState.DISABLED;

    private float currentHealth;
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

        currentHealth = maxHealth;
        StartCoroutine(BehaviorRoutine());
    }

    public override void OnHit(HitData hitData)
    {
        GetDamage(hitData._dealtDamage);
        state = PatientState.PINNED;
        behaviorTimer = hitData._stiffTime;
    }

    public override void GameStarted()
    {
        state = PatientState.IDLE;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
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
                case PatientState.DISABLED:
                    yield return null;
                    break;
                case PatientState.IDLE:
                    if (PlayerDetected())
                    {
                        state = PatientState.CHASE;
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
                            state = PatientState.ROAM;
                        }
                    }

                    yield return null;
                    break;

                case PatientState.ROAM:
                    if (PlayerDetected())
                    {
                        state = PatientState.CHASE;
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
                            state = PatientState.IDLE;
                        }
                    }

                    yield return null;
                    break;

                case PatientState.CHASE:

                    transform.forward = (targetPlayer.transform.position - transform.position).normalized;
                    rBody.AddForce(transform.forward * speed, ForceMode.Force);

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

                case PatientState.PINNED:
                    rBody.velocity = Vector3.zero;
                    if (behaviorTimer < 0)
                    {
                        state = PatientState.IDLE;
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

        transform.rotation = quaternion.LookRotation(dir, Vector3.up);
    }

    public void GetDamage(float value)
    {
        if (value > currentHealth)
        {
            currentHealth = 0f;
            Kill();
            return;
        }

        currentHealth -= value;

    }

    public void Kill()
    {
        StageManager.Instance.RemoveEnemyObject(gameObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * wallDetectRange);
    }
    
    
}