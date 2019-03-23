using UnityEngine;

public class RainbowLevelConfig : ScriptableObject {


	[Header("Instances Configuration")]
	public GameObject[] Prefabs;
	public int[] nWeights;

	[Range(0.01f,10)]
	public float minSpeedInstance = 1f;
	[Range(0.01f,20)]
	public float maxSpeedInstance = 1f;

	[Range(1,10)]
	public int amountOfInstance = 1;

	[Range(0,1)]
	public float coinSpeedVariation;

	public Vector2 curveOfFall;
	[Range(0,50)]
	public float curveXDeltaSpeed;
	[Range(0,50)]
	public float curveOfFallTime;

	[Header("Energy Bar Configuration")]
	[Range(0,100)]
	public int energyDecreaseRate = 1;

	[Range(0,100)]
	public int energyIncreaseLevelUp = 1;

	[Range(0,100)]
	public int energyIncrease = 1;

	[HideInInspector]
	public float energyDecreateTimeRate = 1f;
	/*[Range(0,100)]
	public int badFruitsDecrease = 1;
	[Range(0,100)]
	public int goodFruitsIncrease = 1;*/

	[Range(0,10)]
	public float[] speedVariations;

	/*[Header("Coins Values")]
	[Range(0,100)]
	public int coinValue;
	[Range(0,100)]
	public int goldBarValue;
	[Range(0,100)]
	public int diamontsValue;*/


	/*[Header("Fruits")]
	public GameObject[] goodFruitsPrefabs;
	public GameObject[] badFruitsPrefabs;*/
}
