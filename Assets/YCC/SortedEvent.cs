using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delegate = System.MulticastDelegate;

public class SortedEvent<T> where T : Delegate {

	SortedDictionary<float,HashSet<T>> functions = new SortedDictionary<float,HashSet<T>>();

	public void Add(float priority,T newFunc) {
		if(!functions.ContainsKey(priority)) functions.Add(priority,new HashSet<T>());
		functions[priority].Add(newFunc);
	}
	public void Remove(float priority,T toRemove) {
		if(!functions.ContainsKey(priority)) return;
		if(!functions[priority].Contains(toRemove)) return;
		functions[priority].Remove(toRemove);
		if(functions[priority].Count==0) functions.Remove(priority);
	}

	public void Invoke(params object[]args) {
		foreach(var i in functions) {
			foreach(var j in i.Value) {
				j.DynamicInvoke(args);
			}
		}
	}

}
