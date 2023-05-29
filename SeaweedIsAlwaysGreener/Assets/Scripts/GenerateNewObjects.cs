using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewObjects : MonoBehaviour
{
    public GameObject JunkObj;
    public GameObject RewardObj;
    public Vector3 JunkSpawnValues;
    public Vector3 RewardSpawnValues;
    public int JunkCount;
    public int RewardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;

    public PlayerTurtle player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(JunkSpawnWaves());
        StartCoroutine(RewardSpawnWaves());
    }
    IEnumerator JunkSpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while ((player.GetGameOver() == false) && (player.GetWinLose() == false))
        {
            for (int i = 0; i < JunkCount; i++)
            {
                Vector3 HazSpawnPosition = new Vector3(Random.Range(0.0f, JunkSpawnValues.x), JunkSpawnValues.y, Random.Range(0.0f, JunkSpawnValues.z));
                Quaternion HazSpawnRotation = Quaternion.identity;
                Instantiate(JunkObj, HazSpawnPosition, HazSpawnRotation);
                // print("spawned"+ SpawnPosition);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);
        }
    }
    IEnumerator RewardSpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while (true)
        {
            for (int i = 0; i < RewardCount; i++)
            {
                Vector3 RewSpawnPosition = new Vector3(Random.Range(0.0f, RewardSpawnValues.x), RewardSpawnValues.y, Random.Range(0.0f, RewardSpawnValues.z));
                Quaternion RewSpawnRotation = Quaternion.Euler(0, 270, 0);
                Instantiate(RewardObj, RewSpawnPosition, RewSpawnRotation);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);
        }
    }
}
