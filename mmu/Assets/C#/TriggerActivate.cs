using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    // 所有要开启的物件
    public GameObject[] targets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 逐个开启
            foreach (GameObject obj in targets)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
