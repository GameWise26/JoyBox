using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManagament:MonoBehaviour
{

public Characterdatabase characterDB;
public Text nameText;
public SpriteRenderer artworkSprite;

private int selectedOption = 0;

void Start() 
{
    if(PlayerPrefs.HasKey("selectedOption")){
        selectedOption = 0;
    }
    UpdateCharacter(selectedOption);
}

public void NextOption()
{
selectedOption++;
if(selectedOption++ >= characterDB.CharacterCount)
{
selectedOption = 0;
}
UpdateCharacter(selectedOption);
save();
}

public void Backoption()
{
    selectedOption--;
    if(selectedOption < 0)
    {
    selectedOption = characterDB.CharacterCount -1;
    }
UpdateCharacter(selectedOption);
save();
}


private void UpdateCharacter(int selectedOption)
{
    Character character = characterDB.GetCharacter(selectedOption);
    artworkSprite.Sprite = character.characterSprite;
    nameText.Text = character.characterName;
}

private void load(){
selectedOption = PlayerPrefs.GetInt("selectedOption");
}

private void save(){
    PlayerPrefs.SetInt("selectedOption".selectedOption);
}

public void ChangeScene(int SceneID){
    SceneManagement.loadScene(SceneID);
}
}
