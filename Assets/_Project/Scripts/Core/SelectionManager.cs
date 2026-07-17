using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public bool onTarget;
    public bool overrideText = false;

    public GameObject selectedObject;
    public GameObject interaction_Info_UI;
    public TMP_Text interaction_text;

    public float interactRange = 20f; // raycast giờ chỉ lo ngắm trúng + vật cản; tầm hoạt động thật do Sphere Collider (InteractableObject) quyết định
    public LayerMask ignoreLayer; // tick Terrain và Water layer vào đây

    private void Start()
    {
        onTarget = false;
        // Dùng GetComponent trực tiếp vì interaction_Info_UI chính là TMP_Text object
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
        if (interaction_text == null)
            interaction_text = interaction_Info_UI.GetComponentInChildren<TMP_Text>(true);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

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