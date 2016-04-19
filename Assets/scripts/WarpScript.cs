using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WarpScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit");
        SceneManager.LoadScene("Map");
    }
}
