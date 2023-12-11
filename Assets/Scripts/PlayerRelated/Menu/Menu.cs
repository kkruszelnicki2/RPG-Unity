using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject itemObject;

    [SerializeField] private GameObject player;

    [SerializeField] GameObject MenuActor;
    bool isVisible = false;

    private void Start()
    {
        if(ApplicationModel.isLoaded == 1)
        {
            LoadGame();
            ApplicationModel.isLoaded = 2;
        }
        else if (ApplicationModel.isLoaded == 0)
        {
            NewGame();
            ApplicationModel.isLoaded = 2;
        }
        else
        {
            MoveScene();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setVisible();
        }
    }

    public void setVisible()
    {
        isVisible = !isVisible;
        MenuActor.SetActive(isVisible);

        if (isVisible) 
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;
    }

    public void ChooseOperation(string operation)
    {
        switch (operation)
        {
            case "newGame":
                NewGame();
                break;
            case "loadGame":
                LoadGame();
                break;
            case "saveGame":
                SaveGame();
                break;
        }
    }

    public void NewGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerReset();
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            File.Delete(Application.dataPath + "/save.txt");
        }
        Clean();
    }

    public void LoadGame()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            //Reseting player and his items
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerReset();
            Clean();

            string loaded = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObject load = JsonUtility.FromJson<SaveObject>(loaded);
            //Player
            GameObject.Find("Player").transform.position = load.playerPosition;
            GameObject.Find("Player").GetComponent<PlayerController>().healthSystem.SetHealth(load.playerHealth);
            GameObject.Find("Player").GetComponent<PlayerController>().healthSystem.SetMaxHealth(load.playerMaxHealth);
            GameObject.Find("Player").GetComponent<PlayerController>().levelingSystem.currentExp = load.exp;
            GameObject.Find("Player").GetComponent<PlayerController>().levelingSystem.level = load.level;

            //Items
            for(int i=0; i<load.itemAmount.Count;i++)
            {
                GameObject newItem = Instantiate(itemObject);
                newItem.GetComponent<ItemClass>().InitialiseItem((Item)AssetDatabase.LoadAssetAtPath(load.itemSO[i], typeof(Item)), load.itemAmount[i]);
                GameObject.Find("InventoryUI").GetComponent<Inventory>().AddItem(newItem.GetComponent<ItemClass>());
                Destroy(newItem);
            }
            //Ground items
            for (int i = 0; i < load.groundItemAmount.Count; i++)
            {
                GameObject newGroundItem;
                newGroundItem = Instantiate(itemObject, load.groundItemPos[i], Quaternion.identity);
                newGroundItem.GetComponent<ItemClass>().InitialiseItem((Item)AssetDatabase.LoadAssetAtPath(load.groundItemSO[i], typeof(Item)), load.groundItemAmount[i]);
            }
            //Enemies
            for (int i = 0; i < load.enemyID.Count; i++)
            {
                foreach(GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
                {
                    if(spawner.GetComponent<MobSpawner>().id == load.enemyID[i])
                    {
                        spawner.GetComponent<MobSpawner>().ForceSpawn(load.enemyHealth[i],load.enemyPos[i]);
                    }
                }
            }
            player.GetComponent<PlayerBars>().UpdateHealthBar(load.playerHealth, load.playerMaxHealth); //updating health bar
            player.GetComponent<PlayerBars>().UpdateExpBar(load.exp, GameObject.Find("Player").GetComponent<PlayerController>().levelingSystem.maxExp[load.level]); //updating EXP bar
        }
    }

    public void SaveGame()
    {
        SaveObject saveObject = new SaveObject();
        //Player
        saveObject.playerPosition = GameObject.Find("Player").transform.position;
        saveObject.playerHealth = GameObject.Find("Player").GetComponent<PlayerController>().healthSystem.GetHealth();
        saveObject.playerMaxHealth = GameObject.Find("Player").GetComponent<PlayerController>().healthSystem.GetMaxHealth();
        saveObject.exp = GameObject.Find("Player").GetComponent<PlayerController>().levelingSystem.currentExp;
        saveObject.level = GameObject.Find("Player").GetComponent<PlayerController>().levelingSystem.level;
        //Items
        foreach(GameObject itemSlot in GameObject.Find("InventoryUI").GetComponent<Inventory>().itemSlots)
        {
            if(itemSlot.GetComponent<ItemSlot>().isOccupied())
            {
                saveObject.itemAmount.Add(itemSlot.GetComponent<ItemSlot>().item.GetAmount());
                saveObject.itemSO.Add(AssetDatabase.GetAssetPath(itemSlot.GetComponent<ItemSlot>().item.item));
            }
        }

        foreach (GameObject groundItem in GameObject.FindGameObjectsWithTag("Item"))
        {
            saveObject.groundItemAmount.Add(groundItem.GetComponent<ItemClass>().GetAmount());
            saveObject.groundItemSO.Add(AssetDatabase.GetAssetPath(groundItem.GetComponent<ItemSlot>().item.item));
            saveObject.groundItemPos.Add(groundItem.transform.position);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            saveObject.enemyPos.Add(enemy.transform.position);
            saveObject.enemyHealth.Add(enemy.GetComponent<Slime>().healthSystem.GetHealth());
        }

        string save = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.txt", save);
    }

    private void MoveScene()
    {
        for (int i = 0; i < ApplicationModel.itemAmount.Count; i++)
        {
            Debug.Log(ApplicationModel.itemSO[i].item.itemName + " " + ApplicationModel.itemAmount[i]);
            GameObject newItem = Instantiate(itemObject);
            newItem.GetComponent<ItemClass>().InitialiseItem(ApplicationModel.itemSO[i].item, ApplicationModel.itemAmount[i]);
            GameObject.Find("InventoryUI").GetComponent<Inventory>().AddItem(newItem.GetComponent<ItemClass>());
            Destroy(newItem);
        }

        player.transform.position = ApplicationModel.playerPos;
        player.GetComponent<PlayerController>().healthSystem.SetHealth(ApplicationModel.playerHp);
        player.GetComponent<PlayerController>().levelingSystem.LoadLevel(ApplicationModel.playerLevel);
        player.GetComponent<PlayerController>().levelingSystem.currentExp = ApplicationModel.playerExp;
        player.GetComponent<PlayerBars>().UpdateHealthBar(ApplicationModel.playerHp, ApplicationModel.playerMaxHp);
        player.GetComponent<PlayerBars>().UpdateExpBar(ApplicationModel.playerExp, ApplicationModel.playerMaxExp);
    }

    private class SaveObject
    {
        //Player
        public Vector3 playerPosition;
        public int playerHealth;
        public int playerMaxHealth;
        public int level;
        public int exp;
        //Items in inventory
        public List<int> itemAmount = new List<int>();
        public List<string> itemSO = new List<string>();
        //Items on ground
        public List<Vector3> groundItemPos = new List<Vector3>();
        public List<int> groundItemAmount = new List<int>();
        public List<string> groundItemSO = new List<string>();
        //Enemies
        public List<Vector3> enemyPos = new List<Vector3>();
        public List<int> enemyHealth = new List<int>();
        public List<int> enemyID = new List<int>();
        //Location
        public string locationName = SceneManager.GetActiveScene().name;
    }

    private void Clean()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy.name == "Slime(Clone)")
            {
                enemy.GetComponent<Slime>().GetSpawner().GetComponent<MobSpawner>().ReduceCounter();
            }
            Destroy(enemy);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Destroy(item);
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
