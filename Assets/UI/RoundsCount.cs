using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using TMPro;
using UnityEngine.UI;

public class RoundsCount : MonoBehaviour
{
    TextMeshProUGUI roundText;

    void Start(){
        roundText = GetComponent<TextMeshProUGUI>();
        GameController.instance.OnEnterNextRound += ChangeRoundsCount;
    }
    void ChangeRoundsCount(){
        roundText.text = (GameController.instance.Round - 1).ToString();
    }
}
