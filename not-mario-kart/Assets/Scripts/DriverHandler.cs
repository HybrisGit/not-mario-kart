using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    private class DriverData
    {
        CarDriver driver;
        uint checkpointsReached;

        public DriverData(CarDriver driver)
        {
            this.driver = driver;
            this.checkpointsReached = 0u;
        }
    }

    private static DriverHandler _instance;
    public static DriverHandler  => _instance;

    [HideInInspector]
    private List<DriverData> drivers = new List<DriverData>();
    private DriverData leader = null;

    void Awake()
    {
        _instance = this;
    }

    public void Register(CarDriver driver)
    {
        this.drivers.Add(driver);
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
                    leader.driver.emotionController.SetEmotion(EmotionController.EmotionType.Happy);
                }
                return;
            }
        }
    }
}
