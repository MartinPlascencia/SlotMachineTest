using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class ReelStripsData
{
    public string[][] ReelStrips;
}

[System.Serializable]
public class SpinsData
{
    public Spin[] Spins;
}

[System.Serializable]
public class Spin
{
    public int[] ReelIndex;
    public int ActiveReelCount;
    public int WinAmount;
}

public class GameDataController : MonoBehaviour
{
    private SpinsData _spinsData;
    public SpinsData SpinsData { get { return _spinsData; } }
    private ReelStripsData _reelStripsData;
    public ReelStripsData ReelStripsData { get { return _reelStripsData; } }
    
    public void GetGameData()
    {
        GetSpinsData();
        GetReelsData();
    }

    private string GetJsonText(string fileName){
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile != null)
        {
            return jsonFile.text;
        }
        else
        {
            Debug.LogWarning("JSON file " + fileName + " not found in Resources folder.");
            return "";
        }
    }

    private void GetSpinsData(){
        //_spinsData = JsonUtility.FromJson<SpinsData>(GetJsonText("spins"));
        _spinsData = JsonConvert.DeserializeObject<SpinsData>(GetJsonText("spins"));
        if (_spinsData == null || _spinsData.Spins == null)
        {
            Debug.LogWarning("Failed to deserialize JSON to SpinsData object.");
        }
        else
        {
            Debug.Log("SpinsData object deserialized successfully.");
        }
    }

    private void GetReelsData(){
        //_reelStripsData = JsonUtility.FromJson<ReelStripsData>(GetJsonText("reelstrips"));
        _reelStripsData = JsonConvert.DeserializeObject<ReelStripsData>(GetJsonText("reelstrips"));
        if (_reelStripsData == null || _reelStripsData.ReelStrips == null)
        {
            Debug.LogWarning("Failed to deserialize JSON to ReelStripsData object.");
        }
        else
        {
            Debug.Log("ReelStripsData object deserialized successfully.");
        }
    }
}
