using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public static string PalyerName { get; set; }
    public static string PalyerColour { get; set; }
    public static string Engineer { get; set; }
    public static int PeopleInCamp { get; set; }
    public static int UnAssignedPeopleInCamp { get; set; }
    public static float FireSize { get; set; }
    public static float FireLight { get; set; }
    public static int Tutorial { get; set; }
    public static int Exploration { get; set; }
    public static int MapPostion { get; set; }
    public static int EventConvo { get; set; }
    public static int AssignedGatherWood { get; set; }
    public static int CurrentMapLevel { get; set; }
}
