using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{    
    [SerializeField] int sceneIndex = -1;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Transition());
        }    
    }

    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneIndex); 
        print("Carregueiss");
        Destroy(gameObject);   
    }
}
