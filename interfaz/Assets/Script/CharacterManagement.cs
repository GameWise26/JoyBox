using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManagement : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            load();
        }
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if(selectedOption >= characterDB.CharacterCount){
        selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        save();
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
        selectedOption = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
        save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Debug.Log(selectedOption);
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    private void load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    private void save()
    {
        PlayerPrefs.SetInt("selectedOption" , selectedOption);
    }

}
