using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public GameObject respawnablesParentObject;
    public AudioClip sound_objectRespawned;
    
    private AudioSource source;
    private List<GameObject> respawningObjectsList = new List<GameObject>();

    private Dictionary<int, GameObjectInit> initialPositions = new Dictionary<int, GameObjectInit>();

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.ResetObject += HandleResetObject;
        this.source = gameObject.AddComponent<AudioSource>();
        //UnityEngine.Debug.Log("RespawnManager: start");
        FillList();
        SaveInitialPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillList()
    {
        Transform[] allChildren = respawnablesParentObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            this.respawningObjectsList.Add(child.gameObject);
        }

    }

    void SaveInitialPositions()
    {
        //UnityEngine.Debug.Log("RespawnManager: Saving initial positions");
        foreach (GameObject go in respawningObjectsList)
        {
            //UnityEngine.Debug.Log("RespawnManager: Added object id " + go.GetInstanceID() + " to dictionary");
            this.initialPositions.Add(go.GetInstanceID(), new GameObjectInit(go));
        }
    }

    void HandleResetObject(int instanceId)
    {
        this.initialPositions[instanceId].gameObject.transform.position = this.initialPositions[instanceId].initialPosition;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        int instanceId = other.gameObject.GetInstanceID();
        //Debug.Log("RespawnManager: Object " + instanceId + " fell out of the world.");
        
        if (this.initialPositions.ContainsKey(instanceId))
        {
            //Debug.Log("RespawnManager: Respawning Object " + instanceId);
            this.initialPositions[instanceId].gameObject.transform.position = this.initialPositions[instanceId].initialPosition;
            this.source.PlayOneShot(sound_objectRespawned);
        }
        else if (other.gameObject.tag == "Player")
        {
            // reset scene
            Debug.Log("RespawnManager: Player fell out of the world. Reloading Scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.source.PlayOneShot(sound_objectRespawned);
        }
    }
}

public class GameObjectInit
{
    public GameObject gameObject { get; set; }
    public Vector3 initialPosition { get; set; }

    public GameObjectInit(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.initialPosition = this.gameObject.transform.position;
    }
}
