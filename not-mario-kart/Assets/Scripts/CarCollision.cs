using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(GameCharacterController))]
public class CarCollision : MonoBehaviour
{
    private GameCharacterController characterController;

    private void Awake()
    {
        this.characterController = this.GetComponent<GameCharacterController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        Debug.Log("" + this.name + " collided with " + collision.gameObject.name);
        //TODO: collision severity check
        this.characterController.selectedCharacter.emotions.SetEmotion(EmotionController.EmotionType.Angry);
    }
}
