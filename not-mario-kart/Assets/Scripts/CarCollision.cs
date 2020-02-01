using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CarCollision : MonoBehaviour
{
    public EmotionController emotionController;

    void OnCollisionEnter(Collision collision)
    {
        //TODO: collision severity check
        this.emotionController.SetEmotion(EmotionController.EmotionType.Angry);
    }
}
