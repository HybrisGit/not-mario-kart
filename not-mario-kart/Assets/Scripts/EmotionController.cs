using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public enum EmotionType
    {
        Happy,
        Angry,
        Hurt
    }

    [System.Serializable]
    public struct EmotionData
    {
        public EmotionType emotionType;
        public GameObject emoji;
        public AudioClip audio;

        public void SetActive(bool active)
        {
            this.emotion.SetActive(active);
            // TODO: play/stop audio
        }
    }

    private class PlayedEmotion
    {
        EmotionData emotionData;
        DateTime endTime;

        bool HasEnded => this.endTime >= DateTime.UtcNow;

        public PlayedEmotion(EmotionData emotion)
        {
            this.emotionData = emotion;
            this.endTime = DateTime.UtcNow.AddSeconds(5);
        }
    }

    public EmotionData[] emotionData;
    private PlayedEmotion currentEmotion;


    public void SetEmotion(EmotionType emotion)
    {
        if (this.currentEmotion != null)
        {
            if (this.currentEmotion.emotionData.emoji == emotion)
            {
                // don't play again
                return;
            }

            // disable current
            this.currentEmotion.emotionData.SetActive(false);
        }

        // enable new
        EmotionData data = this.emotionData.First(e => e.emotionType == emotion);
        this.currentEmotion = new PlayedEmotion(data);
    }
}
