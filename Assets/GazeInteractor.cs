using UnityEngine;

public class GazeInteractor : MonoBehaviour
{
    public float maxDistance = 5f;
    public LayerMask uiLayer;

    public VRInterfaceController ui;

    private void Start()
    {
        ui = FindObjectOfType<VRInterfaceController>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, uiLayer))
        {
            Debug.Log("Gazing at: " + hit.collider.name);

            if (ui != null)
                ui.TriggerByGaze();
        }

    }
}

