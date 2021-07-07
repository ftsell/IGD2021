using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBackgroundMusic : MonoBehaviour
{
  private static TESTBackgroundMusic backgroundMusic;

    void Awake()
    {
      if(backgroundMusic == null){
        backgroundMusic = this;
        DontDestroyOnLoad(backgroundMusic);
      }
      else{
        Destroy(gameObject);
      }
    }

}
