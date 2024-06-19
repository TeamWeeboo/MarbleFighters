using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
    public class CharacterList : MonoBehaviour
    {
        [HideInInspector]public List<Character> Characters;
        public Character mainCharacter;

        void Start()
        {
            Characters.Add(mainCharacter);
        }
        public void AddCharacter(Character character){
            Characters.Add(character);
        }
        public void DelCharacter(Character character){
            Characters.Remove(character);
        }

    }

}

