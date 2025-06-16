using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    public int NumToSpawn;
    
    [Tooltip("The amount of time to wait between spawning new boxes")] [SerializeField]
    private float SpawnInterval;

    [FormerlySerializedAs("DonutPrefabs")] [Tooltip("The options for boxes to spawn. Chooses randomly")] [SerializeField]
    private GameObject[] BoxPrefabs;

    [Tooltip("If false, boxes are selected without replacement until all prefabs are cycled through")] [SerializeField]
    private bool ReplaceAfterSelection;

    [Tooltip("The first spawn will be delayed by this many seconds. Not reset by OnEnable")] [SerializeField]
    private float InitialDelay;

    [Tooltip("If true, the prefabs will be spawned in sequential order")] [SerializeField]
    private bool SelectSequentially;
    
    private List<GameObject> _usedBoxes;
    private List<GameObject> _unusedBoxes;
    private ConveyorRolling _roller;
    private float _timer;
    private int _numSpawned;
    private Coroutine _spawnAllCoroutine;

    private void Start()
    {
        _usedBoxes = new List<GameObject>();
        _unusedBoxes = BoxPrefabs.ToList();
        _roller = GameObject.FindFirstObjectByType<ConveyorRolling>();
        _timer = InitialDelay;
    }

    // Keep in mind this will be called if we use the same spawner in consecutive waves
    private void OnEnable()
    {
        _numSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If level is not ongoing, don't spawn anything
        if (GameManager.Instance.IsLevelOngoing != 1)
            return;
        
        // SCALED time, not unscaled
        _timer -= Time.deltaTime;

        if (_timer < 0.0f && _numSpawned < NumToSpawn)
        {
            SpawnBox();
            _timer = SpawnInterval;
        }
    }

    public BoxQualities[] GetBoxPrefabs()
    {
        return BoxPrefabs.Select(prefab => prefab.GetComponent<BoxQualities>()).ToArray();
    }

    public void SetBoxQualities(BoxQualities[] dqs)
    {
        BoxPrefabs = dqs.Select(dq => dq.gameObject).ToArray();
        _usedBoxes = new List<GameObject>();
        _unusedBoxes = BoxPrefabs.ToList();
    }

    private void SpawnBox()
    {
        // Keep track of how many we've spawned
        _numSpawned++;
        
        int index;
        if (!SelectSequentially)  // Select and instantiate a random box
            index = Random.Range(0, _unusedBoxes.Count);
        else  // Choose next box sequentially
            index = 0;

        GameObject d = Instantiate(_unusedBoxes[index]);
        d.transform.position = transform.position;

        // If not replacing, remove from list of possible choices
        if (!ReplaceAfterSelection)
        {
            _usedBoxes.Add(_unusedBoxes[index]);
            _unusedBoxes.RemoveAt(index);
        }

        // If we've run out of choices, add all the possibilities back
        if (_unusedBoxes.Count == 0)
        {
            _unusedBoxes.AddRange(_usedBoxes);
            _usedBoxes.Clear();
        }
        
        // Set the speed of the rollers to match the most recent box spawned
        _roller.SetRollSpeed(d.GetComponent<BoxConveyorMove>().Speed);
    }

    public void SpawnAll()
    {
        if (_spawnAllCoroutine != null)
            return;
        
        _spawnAllCoroutine = StartCoroutine(SpawnAllCoroutine());
    }

    private IEnumerator SpawnAllCoroutine()
    {
        for (int i = 0; i < BoxPrefabs.Length; i++)
        {
            SpawnBox();
            yield return new WaitForSeconds(SpawnInterval);
        }

        _spawnAllCoroutine = null;
    }
}
