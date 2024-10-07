using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p != null)
        {
            if (p.main)
            {
                if (PlayerController.players.Count < 2)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    PlayerController.Cycle();
                    PlayerController.players.Remove(p);
                    Destroy(other.gameObject);
                }
            }
            else
            {
                PlayerController.players.Remove(p);
                Destroy(other.gameObject);
            }
        }
    }
}
