using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class leaderboard : MonoBehaviour
{
    private Transform userCont;
    private Transform userTemp;
    private List<Transform> LbTransformList;

    public void play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void returnMM()
    {
        SceneManager.LoadScene("MenuScene");
    }

    [System.Serializable]
    private class dataEntry
    {
        public string name;
        public float time;
        //public int score;
    }

    private class leaderboardList
    {
        public List<dataEntry> dataEntryList;
    }

    void Awake()
    {
        string filepath = Application.persistentDataPath + "/leaderboard.txt";

        userCont = transform.Find("userCont");
        userTemp = userCont.Find("userTemp");

        userTemp.gameObject.SetActive(false);
        LbTransformList = new List<Transform>();
        if (PlayerPrefs.HasKey("finishTime"))
        {
            if (Convert.ToBoolean(PlayerPrefs.GetInt("finishTime")))
            {
                addToLeaderboard(PlayerPrefs.GetString("name"), PlayerPrefs.GetFloat("time"));
            }
        }
        if (File.Exists(filepath) && new FileInfo(filepath).Length > 0)
        {
            string jString = File.ReadAllText(filepath);
            leaderboardList lbl = JsonUtility.FromJson<leaderboardList>(jString);

            lbl.dataEntryList = lbl.dataEntryList.OrderBy(o => o.time).ToList();

            foreach (dataEntry entry in lbl.dataEntryList)
            {
                leaderboardEntry(entry, userCont, LbTransformList);
            }
        }

    }

    private void leaderboardEntry(dataEntry entry, Transform template, List<Transform> transformList)
    {
        float entryHeight = 40f;
        Transform etransform = Instantiate(userTemp, userCont);
        RectTransform eRT = etransform.GetComponent<RectTransform>();
        eRT.anchoredPosition = new Vector2(0, -entryHeight * transformList.Count);
        etransform.gameObject.SetActive(true);
        transformList.Add(etransform);

        string name = entry.name;
        float time = entry.time;
        etransform.Find("name").GetComponent<Text>().text = name;
        etransform.Find("time").GetComponent<Text>().text = time.ToString("F2"); ;
    }

    public void addToLeaderboard(string name, float time)
    {
        string filepath = Application.persistentDataPath + "/leaderboard.txt";
        dataEntry newDataEntry = new dataEntry { name = name, time = time };
        leaderboardList lbl;
        if (File.Exists(filepath) && new FileInfo(filepath).Length > 0)
        {
            string jString = File.ReadAllText(filepath);

            lbl = JsonUtility.FromJson<leaderboardList>(jString);
            lbl.dataEntryList.Add(newDataEntry);
        }
        else
        {
            FileStream filestream = new FileStream(filepath, FileMode.Create);
            filestream.Dispose();

            lbl = new leaderboardList();
            lbl.dataEntryList = new List<dataEntry>();
            lbl.dataEntryList.Add(newDataEntry);
        }

        string json = JsonUtility.ToJson(lbl);
        File.WriteAllText(filepath, json);

    }

}