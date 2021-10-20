using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] private List<GameObject> waveSpawnObjects;

    public void EnableAllSpawnObjects()
    {
        foreach (var obj in waveSpawnObjects)
        {
            obj.SetActive(true);
            obj.GetComponent<TargetObject>().GameStarted();
        }
    }

    public void RegisterObject(GameObject obj)
    {
        waveSpawnObjects.Add(obj);
    }
    
    public bool RemoveObject(GameObject obj)
    {
        waveSpawnObjects.Remove(obj);
        if (waveSpawnObjects.Count == 0)
            return true;
        
        return false;
    }
}

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;

    public static StageManager Instance => _instance;

    [SerializeField] private bool Debugmode = false;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundModule_Base music;
    [SerializeField] private GameplayUI ui;
   // [SerializeField] private float timeLimit;
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject nextStageGate;
    private int currentWaves;
    //public List<GameObject> enemies;
    
    public delegate void ONGameStarted();

    public ONGameStarted onGameStarted;

    // private float timer;
    // public float Timer => timer;
    //
    // private bool isTimer = true;
    
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
        //timer = timeLimit;
        //if (Debugmode) StartCoroutine(DebugStart());

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // if (timer > 0 && isTimer)
        // {
        //     timer -= Time.deltaTime;
        // }
    }

    public void StartStage()
    {
        music.Play("main");
        onGameStarted();
        StartNewWave();
    }

    public void RegisterEnemyObject(GameObject obj)
    {
        waves[currentWaves].RegisterObject(obj);
    }
    
    public void RemoveEnemyObject(GameObject obj)
    {
        if (waves[currentWaves].RemoveObject(obj))
        {
            if(currentWaves < waves.Length)
                WaveComplete();
            else
            {
                AllWavesComplete();
            }
            
        }
    }

    public void WaveComplete()
    {
        currentWaves++;
        if (currentWaves < waves.Length)
        {
            ui.OnWaveClear(currentWaves);
            Invoke("StartNewWave", 2.0F);
        }
        else
        {
            AllWavesComplete();
        }
    }

    public void StartNewWave()
    {
        waves[currentWaves].EnableAllSpawnObjects();
    }

    public void AllWavesComplete()
    {
        //isTimer = false;
        music.Stop();
        music.Play("ambient");
        GetComponent<SoundModule_Base>().Play("clear");
        ui.OnClear();
        nextStageGate.SetActive(true);
    }
    
    private IEnumerator DebugStart()
    {
        yield return new WaitForEndOfFrame();
        onGameStarted();
    }
    
}
