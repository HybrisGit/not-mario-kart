using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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
        public GameObject[] objects;

        public void SetSelected(bool selected)
        {
            foreach (GameObject object in this.objects)
            {
                object.SetActive(selected);
            }
        }
    }

    public CharacterSettings[] characters;
    private static List<CharacterType> takenCharacters = new List<CharacterType>();

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
            characterSettings.SetSelected(characterSettings.character == character);
        }
    }
}
