using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable
{
    [SerializeField] string locationName;
    [SerializeField] Vector3 playerPos;
    private GameObject player;

    public void Awake()
    {
        player = GameObject.Find("Player");
    }

    override protected void Interaction()
    {
        SaveData saveObject = new SaveData();
        saveObject.playePos = playerPos;
        saveObject.locationName = locationName;
        saveObject.playerLevel = player.GetComponent<PlayerController>().levelingSystem.level;
        saveObject.playerExp = player.GetComponent<PlayerController>().levelingSystem.currentExp;
        saveObject.playerMaxExp = player.GetComponent<PlayerController>().levelingSystem.GetMaxExp();
        saveObject.playerHp = player.GetComponent<PlayerController>().healthSystem.GetHealth();
        saveObject.playerMaxHp = player.GetComponent<PlayerController>().healthSystem.GetMaxHealth();

        ApplicationModel.saveData(saveObject);
        SceneManager.LoadScene(locationName);
    }

    public class SaveData // BUNDLE TO STORE PLAYER'S DATA
    {
        //Player
        public Vector2 playePos; //PLAYER POSITION
        public int playerLevel;
        public int playerExp;
        public int playerMaxExp;
        public int playerHp;
        public int playerMaxHp;

        //Location
        public string locationName;
    }
}
