using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataLoader : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    StaticCnfLoader.LoadAllCsvBinary();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
