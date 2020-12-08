using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _singleton = null;
    public static AudioManager singleton { get => _singleton; }

    private AudioSource src = null;
    public AudioSource Src { get => src; }

    private void Awake()
    {
        if (_singleton == null)
            _singleton = this;
        else if (_singleton != this)
            Debug.Log("Duplicate AudioManager found");
    }

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }
}

