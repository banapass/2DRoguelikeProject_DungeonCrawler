using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    public Room(HashSet<Vector2> roomPos, RoomType roomType)
    {
        this.RoomPosition = roomPos;
        this.roomType = roomType;
    }
    public HashSet<Vector2> RoomPosition;
    public RoomType roomType;
    public enum RoomType
    {
        PlayerRoom,
        EnemyRoom

    }


}
public class GameManager : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    [SerializeField] Enemy[] enemys;
    [SerializeField] Player player;
    [SerializeField][Range(1, 100)] int createPercent = 20;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rooms = CorridorFirstDungeonGenerator.CheckCurrentRoom(player.transform.position);

        SpawnEnemy();


    }

    void SpawnEnemy()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int createCount = Random.Range(2, 5);
            if (rooms[i].roomType == Room.RoomType.PlayerRoom)
            {
                continue;
            }
            foreach (var pos in rooms[i].RoomPosition)
            {
                int percent = Random.Range(1, 101);
                if (percent <= createPercent && createCount > 0)
                {
                    GameObject temp = Instantiate(enemys[0].gameObject);
                    temp.transform.position = pos;
                    createCount--;
                }
            }
        }

    }

    // private void OnDrawGizmos()
    // {

    //     for (int i = 0; i < rooms.Count; i++)
    //     {
    //         Gizmos.color = Color.blue;
    //         if (rooms[i].roomType == Room.RoomType.PlayerRoom)
    //         {
    //             Gizmos.color = Color.red;
    //         }
    //         foreach (var pos in rooms[i].RoomPosition)
    //         {
    //             Gizmos.DrawWireCube(pos, new Vector3(1, 1, 1));
    //         }

    //     }
    // }
}
