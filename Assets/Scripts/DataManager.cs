using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DataManager : MonoBehaviour 
{
	public string fileName;

	public static DataManager instance;

	char[] delimeterCharacteres = { '|', ',' };
	string[] tmpString;
	public int items;
	public int lives;

	void Awake () 
	{
		instance = this;
		LoadData ();
	}

	public void LoadData()
	{
		if (File.Exists (fileName)) 
		{
			StreamReader sr = new StreamReader (fileName);
			string stringLine;
			while((stringLine = sr.ReadLine()) != null)
			{
				tmpString = stringLine.Split (delimeterCharacteres);
				items = int.Parse (tmpString [0]);
				lives = int.Parse (tmpString [1]);
			}
			sr.Close ();
		} 
		else 
		{
			items = 0;
			lives = 1000;
		}
	}

	public void SaveData()
	{
		StreamWriter sw = new StreamWriter (fileName);
		string tmpData = items + "|" + lives;
		sw.WriteLine (tmpData);
		sw.Close ();
	}
}
