using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	[Serializable]
	public class SaveFile
	{
		public int highScore;

		public SaveFile()
		{
			highScore = GameHandler.highScore;
		}
	}

	//http://amalgamatelabs.com/Blog/4/data_persistence
	[DllImport("__Internal")]
	private static extern void SyncFiles();

	[DllImport("__Internal")]
	private static extern void WindowAlert(string message);
	public static void SaveHighScore()
	{
		string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;

		try
		{
			if (File.Exists(dataPath))
			{
				File.WriteAllText(dataPath, string.Empty);
				fileStream = File.Open(dataPath, FileMode.Open);
			}
			else
			{
				fileStream = File.Create(dataPath);
			}

			binaryFormatter.Serialize(fileStream, new SaveFile());
			fileStream.Close();

			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				SyncFiles();
			}
		}
		catch (Exception e)
		{
			PlatformSafeMessage("Failed to Save: " + e.Message);
		}
	}

	public static void LoadHighScore()
	{
		string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);

		try
		{
			if (File.Exists(dataPath))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dataPath, FileMode.Open);

				GameHandler.highScore = ((SaveFile)binaryFormatter.Deserialize(fileStream)).highScore;
				fileStream.Close();
			}
		}
		catch (Exception e)
		{
			PlatformSafeMessage("Failed to Load: " + e.Message);
		}
	}

	private static void PlatformSafeMessage(string message)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			WindowAlert(message);
		}
		else
		{
			Debug.Log(message);
		}
	}

}
