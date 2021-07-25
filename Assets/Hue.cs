using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APSdk;

public class Hue : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);

        APRewardedAd.Show("test", (value)=> { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
