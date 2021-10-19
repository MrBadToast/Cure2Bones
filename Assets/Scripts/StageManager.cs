using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;

    public static StageManager Instance => _instance;

    [SerializeField] private bool Debugmode = false;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundModule_Base music;
    [SerializeField] private GameplayUI ui;
    [SerializeField] private float timeLimit;
    public List<GameObject> enemies;
    
    public delegate void ONGameStarted();

    public ONGameStarted onGameStarted;

    private float timer;
    public float Timer => timer;

    private bool isTimer = true;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        timer = timeLimit;
        if (Debugmode) StartCoroutine(DebugStart());

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (timer > 0 && isTimer)
        {
            timer -= Time.deltaTime;
        }
    }

    public void StartStage()
    {
        music.Play("main");
        onGameStarted();
    }

    public void RegisterEnemyObject(GameObject obj)
    {
        enemies.Add(obj);
    }
    
    public void RemoveEnemyObject(GameObject obj)
    {
        enemies.Remove(obj);
        if(enemies.Count == 0)
            StageComplete();
    }

    public void StageComplete()
    {
        isTimer = false;
        music.Stop();
        music.Play("ambient");
        GetComponent<SoundModule_Base>().Play("clear");
        ui.OnClear();
    }
    
    private IEnumerator DebugStart()
    {
        yield return new WaitForEndOfFrame();
        onGameStarted();
    }
    
}
