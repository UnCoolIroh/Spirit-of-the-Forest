using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Source: https://www.youtube.com/watch?v=eK2SlZxNjiU&ab_channel=Rootbin
public class RoomManager : MonoBehaviour
{
    [SerializeField] List<GameObject> roomPrefabs;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    public int SceneBuildIndex;

    int roomWidth = 20;
    int roomHeight = 12;

    int gridSizeX = 10;
    int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();
    private List<GameObject> notCleared = new List<GameObject>();


    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private bool generationComplete = false;

    private int roomCount;
    private bool floorcleared;
    public AudioManager audioManager;
    public int roomsLeft;
    public TextMeshProUGUI rooms;
    public TextMeshProUGUI floor;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
        floor.text = "Floor " + (SceneBuildIndex - 2);

    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));

        }
        else if(roomCount < minRooms)
        {
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            roomsLeft = notCleared.Count;
        }
        foreach (GameObject room in roomObjects)
        {
            Room room1 = room.GetComponent<Room>();
            if (!room1.roomCleared)
            {
                floorcleared = false;
                break;
            }
            else
            {
                floorcleared = true;
            }
        }
        foreach (GameObject room in roomObjects) {
            Room room1 = room.GetComponent<Room>();
            if (room1.roomCleared) {
                if (notCleared.Contains(room)) {
                    notCleared.Remove(room);
                    roomsLeft = notCleared.Count;
                }
            }
        }
        if (floorcleared)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
            SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
            audioManager.playSFX(audioManager.whoosh);
        }
        rooms.text = "Rooms Left: " + roomsLeft;

    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefabs[0], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
        notCleared.Add(initialRoom);

    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        if (x >= gridSizeX || y >= gridSizeY)
        {
            return false;
        }
        if (roomCount >= maxRooms)
        {
            return false;
        }
        if (UnityEngine.Random.value < .5f && roomIndex != Vector2Int.zero)
        {
            return false; 
        }
        if (roomGrid[x, y] != 0)
        {
            return false;
        }
        if (CountAdjacentRooms(roomIndex) > 1)
            return false;
        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;
        var newRoom = Instantiate(roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Count)], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);
        notCleared.Add(newRoom);

        OpenDoors(newRoom, x, y);

        return true; 
    }

    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        notCleared.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if ( y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
        {
            return roomObject.GetComponent<Room>();
        }
        return null;
    }
    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;
        if (x > 0 && roomGrid[x - 1, y] != 0) count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++;
        if (y > 0 && roomGrid[x, y  - 1] != 0) count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;
        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex) {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX/2), 
            roomHeight * (gridY - gridSizeY/2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));

            }
        }

    }

}
