using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameCharacterController : MonoBehaviour
{
    public enum CharacterType
    {
        Princess,
        Pilot,
        Alien,
        Viking,
        ENUM_SIZE
    }

    [System.Serializable]
    public class CharacterSettings
    {
        public CharacterType character;
        public EmotionController emotions;
        public GameObject[] objects;

        public void SetSelected(bool selected)
        {
            foreach (GameObject obj in this.objects)
            {
                obj.SetActive(selected);
            }
        }
    }

    public CharacterSettings[] characters;
    private static List<CharacterType> takenCharacters = new List<CharacterType>();
    public CharacterSettings selectedCharacter;

    void Start()
    {
        CharacterType selectedCharacter = (CharacterType)(takenCharacters.Count % (int)CharacterType.ENUM_SIZE);
        takenCharacters.Add(selectedCharacter);
        this.SelectCharacter(selectedCharacter);
    }

    public void SelectCharacter(CharacterType character)
    {
        foreach (CharacterSettings characterSettings in this.characters)
        {
            if (characterSettings.character == character)
            {
                this.selectedCharacter = characterSettings;
                continue;
            }
            // disable all other characters
            characterSettings.SetSelected(false);
        }

        Debug.Log("Select character " + this.selectedCharacter.character);
        // enable selected
        this.selectedCharacter.SetSelected(true);
        this.selectedCharacter.emotions.SetEmotion(EmotionController.EmotionType.Happy);
    }
}
