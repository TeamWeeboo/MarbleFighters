using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerLevel : MonoBehaviour
{
    public string playerName;
    public string description;
    public int playerLevel;
    public int maxLevel;
    public int currentLevel;
    public int[] nextLevelExp;
    public int currentExp;
    public int currentHp, maxHp;
    public int currentAd, originAd, currentAp, originAp, currentSpeed, originSpeed, currentArmor, originArmor;

    void Start()
    {
        nextLevelExp = new int[maxLevel];
        nextLevelExp[0] = 1000;
        for(int i = 1; i < maxLevel; i++){
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i-1] * 1.1f);  //升级经验的算法 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            AddExp();    //测试用加经验
        }
    }

    public void AddExp(){
        currentExp += 9999;
        if(currentExp > nextLevelExp[playerLevel - 1] && playerLevel < maxLevel){
            LevelUp();
        }
    }
    public void LevelUp(){   //升级加的属性
        currentExp -= nextLevelExp[playerLevel - 1];
        playerLevel++;
        maxHp += 200;
        if(currentExp > nextLevelExp[playerLevel - 1] && playerLevel < maxLevel){
            LevelUp();
        }
        currentHp = maxHp;
    }
}
