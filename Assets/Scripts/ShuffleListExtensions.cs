using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class ShuffleListExtensions {

	public static IList<T> Suffle<T>(this IList<T> array){

		for (int i = 0; i < array.Count; i++) {

			var index = Mathf.FloorToInt (UnityEngine.Random.value * (array.Count - i)) + i;

			var swap = array [i];
			array [i] = array [index];
			array [index] = swap;

		}

		return array;

	}

	public static IList<T> ReturnReverseIList<T>(this IList<T> _List){
		IList<T> reserveList = _List;

		reserveList.Reverse ();

		return reserveList;
	}

	public static List<T> ReturnReverseList<T>(this List<T> _List){
		List<T> reserveList = _List;

		reserveList.Reverse ();

		return reserveList;
	}
}
