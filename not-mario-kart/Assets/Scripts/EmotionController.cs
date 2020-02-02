using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public class EmotionController : MonoBehaviour
{
    public enum EmotionType
    {
        Happy,
        Angry,
        Hurt
    }

    [System.Serializable]
    public class EmotionData
    {
        public EmotionType emotionType;
        public Sprite emoji;
        public AudioClip[] audioClips;

        private int lastClip = 0;

        private static Random rng = new Random();

        public void SetActive(bool active, AudioSource audioSource, SpriteRenderer spriteRenderer)
        {
            if (active)
            {
                // pick random clip
                int newClipIndex;
                if (this.audioClips.Length == 1)
                {
                    newClipIndex = 0;
                }
                else
                {
                    newClipIndex = rng.Next(0, this.audioClips.Length - 1);
                    if (newClipIndex >= this.lastClip)
                    {
                        newClipIndex++;
                    }
                }

                // play selected clip
                this.lastClip = newClipIndex;
                audioSource.PlayOneShot(this.audioClips[newClipIndex]);
                Debug.Log("Play sound " + this.audioClips[newClipIndex] + " for " + audioSource.GetComponentInParent<GameCharacterController>().selectedCharacter.character);

                // show sprite
                spriteRenderer.sprite = this.emoji;
            }
            else
            {
                // hide sprite
                spriteRenderer.sprite = null;
            }
        }
    }

    private class PlayedEmotion
    {
        public EmotionData emotionData;
        public DateTime endTime;

        public bool HasEnded => this.endTime <= DateTime.UtcNow;

        public PlayedEmotion(EmotionData emotion)
        {
            this.emotionData = emotion;
            this.endTime = DateTime.UtcNow.AddSeconds(5);
        }
    }

    public EmotionData[] emotionData;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    private PlayedEmotion currentEmotion;

    private void Update()
    {
        if (this.currentEmotion != null)
        {
            if (this.currentEmotion.HasEnded)
            {
                this.currentEmotion.emotionData.SetActive(false, this.audioSource, this.spriteRenderer);
                this.currentEmotion = null;
            }
        }
    }

    public void SetEmotion(EmotionType emotion)
    {
        if (this.currentEmotion != null)
        {
            if (this.currentEmotion.emotionData.emotionType == emotion)
            {
                // don't play again
                return;
            }

            // disable current
            this.currentEmotion.emotionData.SetActive(false, this.audioSource, this.spriteRenderer);
        }

        // enable new
        EmotionData data = this.emotionData.First(e => e.emotionType == emotion);
        this.currentEmotion = new PlayedEmotion(data);
        this.currentEmotion.emotionData.SetActive(true, this.audioSource, this.spriteRenderer);
    }
}
