﻿using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// Abstract class for making reload-proof singletons out of ScriptableObjects
/// Returns the asset created on editor, null if there is none
/// Based on https://www.youtube.com/watch?v=VBA1QCoEAX4
/// </summary>
/// <typeparam name="T">Type of the singleton</typeparam>

public abstract class SingletonScriptableObject<T> : SerializedScriptableObject where T : SerializedScriptableObject {
	private static T _instance = null;
	public static T Instance
	{
		get
		{
			if (!_instance)
				_instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
			return _instance;
		}
	}
}