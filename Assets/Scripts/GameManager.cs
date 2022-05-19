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
    public static List<Enemy> researchTarget = new List<Enemy>();
    [SerializeField] Player player;
    [SerializeField][Range(1, 100)] int createPercent = 20;
    [Header("Item")]
    public PickUp dropItem;
    [SerializeField] Potion[] potions;
    [SerializeField] Weapon[] weapons;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rooms = CorridorFirstDungeonGenerator.CheckCurrentRoom(player.transform.position);

        SpawnEnemy();
        DropItem();
    }

    // 방에 적 생성
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
                int randomNum = Random.Range(1, 101);
                if (randomNum <= createPercent && createCount > 0)
                {
                    GameObject temp = Instantiate(enemys[0].gameObject);
                    temp.transform.position = pos;
                    createCount--;
                }
            }
        }
    }

    public static void ResearchEnemys()
    {
        if (researchTarget.Count < 2)
            return;

        for (int i = 1; i < researchTarget.Count; i++)
        {
            researchTarget[i].ResearchPath();
        }
        researchTarget.Clear();
    }

    Item DropItem()
    {
        int randomNum = Random.Range(0, System.Enum.GetValues(typeof(Item.ItemType)).Length);
        Item choiceItem = null;

        switch (randomNum)
        {
            case 0:
                choiceItem = potions[Random.Range(0, potions.Length)];
                break;
            case 1:
                choiceItem = weapons[Random.Range(0, weapons.Length)];
                break;

        }
        return choiceItem;
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
