using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public int triggerId = 0;
    public bool triggerOnlyOnce = true;
    public bool sendSwitchEnable = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Trigger hit");
            if(sendSwitchEnable)
            {
                EventsManager.instance.OnSwitchEnable(this.triggerId);
            } else
            {
                EventsManager.instance.OnPlayerEnteredTrigger(this.triggerId);
            }

            if(triggerOnlyOnce)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
