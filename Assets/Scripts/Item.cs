using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName ="Item/New Item",fileName ="New Item")]
abstract public class Item : MonoBehaviour ,IUsable
{
    string itemName;
    string itemToolTip;

    abstract public void Use();
}
