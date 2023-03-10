using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    private Material initialMaterial;
    public Material changeMaterial;
    
    void Start()
    {
        initialMaterial = GetComponent<TilemapRenderer>().material;
        StartCoroutine(ChangeTime(5f));

    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator ChangeTime(float timeChange)
    {

        while (true)
        {
            yield return new WaitForSeconds(timeChange);
            if (gameObject.GetComponent<TilemapRenderer>().material == initialMaterial)
            {
                gameObject.GetComponent<TilemapRenderer>().material = changeMaterial;


            }
            else
            {
                gameObject.GetComponent<TilemapRenderer>().material = initialMaterial;
            }

        }
  
           
      
        

    }
}
