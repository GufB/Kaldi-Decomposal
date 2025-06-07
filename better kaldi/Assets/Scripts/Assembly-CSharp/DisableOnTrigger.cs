using System.Collections;
using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    private string triggeringTag = "Player";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag(triggeringTag))
        {
            hasTriggered = true;
            StartCoroutine(DisableObjectsAfterDelay(0.0001f));
        }
    }

    private IEnumerator DisableObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
