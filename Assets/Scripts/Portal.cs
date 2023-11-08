using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    bool isCollision = false;

    [SerializeField] string locationName;
    [SerializeField] Vector3 playerPos;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isCollision) //AFTER PRESSING E ON PORTAL, PLAYER WILL BE TELEPORTED
        {
            SaveData saveObject = new SaveData();
            saveObject.playePos = playerPos;
            saveObject.locationName = locationName;

            ApplicationModel.saveData(saveObject);
            SceneManager.LoadScene(locationName);
        }
    }

    public class SaveData // BUNDLE TO STORE PLAYER'S DATA
    {
        //Player
        public Vector2 playePos; //PLAYER POSITION

        //Location
        public string locationName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollision = false;
        }
    }
}
