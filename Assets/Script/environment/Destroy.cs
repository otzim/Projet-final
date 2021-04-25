using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
  


    private void Update()
    {
        if (Input.GetKeyDown("f"))
            BreakTheThing();
    }
    public void BreakTheThing()
    {
       
        Destroy(gameObject);
    }
  

  

    
}
