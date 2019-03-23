/* Written by Scott Cabot 2013 © */

using UnityEngine;
using System.Collections;

public static class UniqueID {

    private static string[] characters = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
                               "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v","w", "x", "y", "z","A",
							   "B", "C", "D", "E", "F", "G", "H", "I", "J", "K","L", "M", "N", "O", "P", "Q",
							   "R", "S", "T", "U", "V","W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6",
                               "7", "8", "9"};

	private static ArrayList uniqueIDList = new ArrayList();

	/* Creates a random 8 digit alpha numeric string.
	Checks the string is not in the arraylist to keep it truly unique. */	
	public static string getID() {
		string uniqueID;
		do {
		uniqueID = "";
		for(int i = 0; i < 100; i++) {
			uniqueID += characters[Random.Range(0, characters.Length)];	
			}
		} while(uniqueIDList.Contains(uniqueID));
		uniqueIDList.Add(uniqueID);
		return uniqueID;
	}

	/* Removes the ID from the list so that it can be reused. */
	public static void removeID (string uniqueID) {
		uniqueIDList.Remove(uniqueID);
	}
}