using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
	public enum Factions {
		Player = 0,
		Npc = 1,
	}


	public static class FactionUtils {

		static int[,] relations = new int[2,2]{
	{ 1,-1},
	{-1, 1}
	};

		public static int GetRelation(Factions from,Factions to) => relations[(int)from,(int)to];
	}
}