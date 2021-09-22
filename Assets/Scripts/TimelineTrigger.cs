using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public bool playOnlyOnce = true;
    public GameObject timeLine = null;

    private PlayableDirector myDirector = null;
    private bool timeLinePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        myDirector = timeLine.GetComponent<PlayableDirector>();
        myDirector.stopped += OnTimeLineStopped;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!timeLinePlaying && other.tag == "Player")
        {
            timeLinePlaying = true;
            myDirector.Play();
            //Debug.Log("TimelineTrigger: Player entered trigger");
        }
    }

    void OnTimeLineStopped(PlayableDirector aDirector)
    {
        if (playOnlyOnce && aDirector == myDirector)
        {
            gameObject.GetComponent<TimelineTrigger>().enabled = false;
        } else
        {
            timeLinePlaying = false;
        }

    }
}
