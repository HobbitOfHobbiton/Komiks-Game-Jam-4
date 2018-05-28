using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateClamp : MonoBehaviour
{
    [Range(20, 60)]
    public int framerate = 30;

	void Awake ()
    {
        Application.targetFrameRate = framerate;
	}

}
