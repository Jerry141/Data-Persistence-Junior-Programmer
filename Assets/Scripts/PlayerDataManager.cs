using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class PlayerDataManager : MonoBehaviour
{
    public string PlayerName;
    public string BestPlayer;
    public int HighScore;
    public static PlayerDataManager Instance;

    private void Awake()
    {
        Instance = this;
        LoadName();
    }

    public void GetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void SetBestPlayer()
    {
        if (MainManager.Instance.m_Points > HighScore)
        {
            HighScore = MainManager.Instance.m_Points;
            BestPlayer = PlayerName;
            SaveName();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public string BestPlayer;
        public int HighScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.HighScore = HighScore;
        data.BestPlayer = BestPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            HighScore = data.HighScore;
            BestPlayer = data.BestPlayer;
        }
    }

}
