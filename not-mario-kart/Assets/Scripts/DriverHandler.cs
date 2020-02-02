using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    private class DriverData
    {
        public CarDriver driver;
        public uint checkpointsReached;

        public DriverData(CarDriver driver)
        {
            this.driver = driver;
            this.checkpointsReached = 0u;
        }
    }

    public static DriverHandler Instance;
    public int carsToSpawn;
    public int spawnDelaySeconds;
    public GameObject carPrefab;

    [HideInInspector]
    private List<DriverData> drivers = new List<DriverData>();
    private DriverData leader = null;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(this.SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        for (int i = 0; i < this.carsToSpawn; ++i)
        {
            // get spawn
            Transform spawn = RaceTrack.Instance.checkpoints[0].transform;
            // create at spawn
            Instantiate(this.carPrefab, spawn.position, spawn.rotation, this.transform);

            // wait
            yield return new WaitForSeconds(this.spawnDelaySeconds);
        }
    }

    public void Register(CarDriver driver)
    {
        this.drivers.Add(new DriverData(driver));
    }

    public void OnCheckpointReached(CarDriver driver)
    {
        foreach (DriverData data in this.drivers)
        {
            if (data.driver == driver)
            {
                data.checkpointsReached++;

                // update leader
                if (data != leader && data.checkpointsReached > leader.checkpointsReached)
                {
                    leader = data;
                    // emote leader
                    leader.driver.characterController.selectedCharacter.emotions.SetEmotion(EmotionController.EmotionType.Happy);
                }
                return;
            }
        }
    }
}
