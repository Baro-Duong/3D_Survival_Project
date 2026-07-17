using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Raycasts from the camera each frame to find what the player is looking at, and shows its name/HP as UI text
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public bool onTarget;
    public bool overrideText = false;

    public GameObject selectedObject;
    public GameObject interaction_Info_UI;
    public TMP_Text interaction_text;

    public float interactRange = 20f; // raycast only handles aim + occlusion; actual range is gated by each item's Sphere Collider
    public LayerMask ignoreLayer;     // tick Terrain/Water layers here to exclude them from the raycast

    // Caches the TMP_Text component on (or inside) interaction_Info_UI
    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
        if (interaction_text == null)
            interaction_text = interaction_Info_UI.GetComponentInChildren<TMP_Text>(true);
    }

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Raycasts forward each frame and shows the target's name (item) or HP (rabbit) in the UI text
    void Update()
    {
        if (overrideText) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, ~ignoreLayer))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponentInParent<InteractableObject>();
            if (interactable != null)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                return;
            }

            RabbitHealth rabbit = selectionTransform.GetComponentInParent<RabbitHealth>();
            if (rabbit != null)
            {
                onTarget = true;
                selectedObject = rabbit.gameObject;
                interaction_text.text = "Rabbit [" + (int)rabbit.currentHP + "/" + (int)rabbit.config.rabbitMaxHP + "]";
                interaction_Info_UI.SetActive(true);
                return;
            }

            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}
