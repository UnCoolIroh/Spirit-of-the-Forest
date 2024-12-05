using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Source: https://www.youtube.com/watch?v=eK2SlZxNjiU&ab_channel=Rootbin
public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject topDoorWall;
    [SerializeField] GameObject bottomDoorWall;
    [SerializeField] GameObject leftDoorWall;
    [SerializeField] GameObject rightDoorWall;
    private bool topDoorOpen;
    private bool leftDoorOpen;
    private bool rightDoorOpen;
    private bool bottomDoorOpen;

    public List<GameObject> EnemySpawner;
    public bool roomCleared = false;
    public GameObject roomClearloot;
    public bool entered = false;

    public Vector2Int RoomIndex { get; set; }

    void Update()
    {
        CheckLocks();
    }

    private void CheckLocks()
    {
        if (GameObject.FindGameObjectWithTag("fly") != null || GameObject.FindGameObjectWithTag("hive") != null)
        {
            LockDoor();
        }
        else
        {
            if (entered)
            {
                unlockDoors();
                roomCleared = true;
            }
        }
    }

    private void unlockDoors()
    {
        if (topDoorOpen)
        {
            topDoor.SetActive(true);
            topDoorWall.SetActive(false);

        }
        if (leftDoorOpen)
        {
            leftDoor.SetActive(true);
            leftDoorWall.SetActive(false);
        }
        if (rightDoorOpen)
        {
            rightDoor.SetActive(true);
            rightDoorWall.SetActive(false);
        }
        if (bottomDoorOpen)
        {
            bottomDoor.SetActive(true);
            bottomDoorWall.SetActive(false);
        }
    }

    public void LockDoor()
    {
  
        topDoor.SetActive(false);
        topDoorWall.SetActive(true);
    
        bottomDoor.SetActive(false);
        bottomDoorWall.SetActive(true);

        leftDoor.SetActive(false);
        leftDoorWall.SetActive(true);

        rightDoor.SetActive(false);
        rightDoorWall.SetActive(true);
        
    }


    public void OpenDoor(Vector2Int direction)
    {
        
        if (direction == Vector2Int.up)
        {
            topDoorOpen = true;
            topDoor.SetActive(true);
            topDoorWall.SetActive(false);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoorOpen = true;
            bottomDoor.SetActive(true);
            bottomDoorWall.SetActive(false);
        }
        if (direction == Vector2Int.left)
        {
            leftDoorOpen = true;
            leftDoor.SetActive(true);
            leftDoorWall.SetActive(false);
        }
        if (direction == Vector2Int.right)
        {
            rightDoorOpen = true;
            rightDoor.SetActive(true);
            rightDoorWall.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        entered = true;
        if (!roomCleared)
        {
            if (collision.CompareTag("Player"))
            {
                foreach (GameObject spawner in EnemySpawner)
                {
                    spawner.GetComponent<EnemySpawner>().Spawning();
                }
            }
        }
    }
}
