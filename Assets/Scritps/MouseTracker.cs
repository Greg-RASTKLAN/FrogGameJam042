using UnityEngine;

public class MouseTracker : MonoBehaviour
{




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        // Converti la position de la souris en coordonées du monde
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Met à jour la position de l'objet
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);



        /*Debug.Log(mousePos.x);
        Debug.Log(mousePos.y);*/
    }
}
