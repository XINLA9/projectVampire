using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply_magic : MonoBehaviour
{
    private Attributes attributes;
    public GameObject AOE;
    bool canClick = false;
    // Start is called before the first frame update
    void Start()
    {
        attributes = GetComponent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * 2f);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 clickPosition = ray.GetPoint(rayDistance);
            if (Input.GetMouseButtonDown(0) && (attributes.mana >= 30) & canClick) {
                attributes.mana -= 30;
                Instantiate(AOE, clickPosition, AOE.transform.rotation);
            }
        } 
    }

    private void OnMouseDown() {
        StartCoroutine(WaithalfSeconds());
    }

    IEnumerator WaithalfSeconds() {
         yield return new WaitForSeconds(1/2);
         canClick = true;
    }
}
