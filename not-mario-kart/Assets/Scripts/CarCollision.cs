using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CarCollision : MonoBehaviour
{
    public GameCharacterController characterController;

    void OnCollisionEnter(Collision collision)
    {
        //TODO: collision severity check
        this.characterController.selectedCharacter.emotions.SetEmotion(EmotionController.EmotionType.Angry);
    }
}
