using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using UnityEngine.UI;


public class WeaponUI : MonoBehaviour
{
    Image weaponImage;

    void Start()
    {
        weaponImage = GetComponent<Image>();
        GameController.instance.OnEnterNextRound += ChangeWeaponIcon;
    }
    public void ChangeWeaponIcon()
    {
        weaponImage.sprite = GameController.instance.playerCharacters[0].moveSet.weaponSprite;
    }
}
