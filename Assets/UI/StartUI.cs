using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Combat{
    public class StartUI : MonoBehaviour
{
    public GameObject setting;
    public Character character;
    
    void Awake(){
        character = GetComponent<Character>();
    }
    public void SettingOpen(){
        setting.SetActive(true);
    }
    public void SettingClose(){
        setting.SetActive(false);
    }   
    public void AdUp(){
        
    }
}
}

