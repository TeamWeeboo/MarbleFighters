using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using TMPro;
using UnityEngine.UI;

public class ChararterUI : MonoBehaviour
{
    public CharacterList list;
    public TextMeshProUGUI statusText;
    private int nextCharacter;

    void Start(){
        statusText.gameObject.SetActive(false);
    }
    
    public void ShowDetails(){
        statusText.gameObject.SetActive(true);
        statusText.text =  "����" + list.Characters[0].originAd.ToString() + "\n" + "����" + list.Characters[0].originAgile.ToString() + "\n" + "���" + list.Characters[0].originAp.ToString() + "\n" ;
        nextCharacter = 0;
    }

    public void NextCharacter(){
        nextCharacter=(nextCharacter+1)%list.Characters.Count;
        statusText.text =  "����" + list.Characters[nextCharacter].originAd.ToString() + "\n" + "����" + list.Characters[nextCharacter].originAgile.ToString() + "\n" + "���" + list.Characters[nextCharacter].originAp.ToString() + "\n" ;
    }
    public void Ad(){
        list.Characters[nextCharacter].AdUp();
    }
    public void Agile(){
        list.Characters[nextCharacter].AgileUp();
    }
    

    
}
