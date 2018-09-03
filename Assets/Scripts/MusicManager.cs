using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour {

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    Queue<int> audioQueue;

    int previousLevelID;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
	}

    private void Start()
    {

        if(!source)
        {
            source = GetComponent<AudioSource>();
        }

        if(clips.Length > 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            source.clip = clips[0];
            source.Play();
        }
        
        if(audioQueue == null)
        {
            audioQueue = new Queue<int>();
        }
    }

    private void Update()
    {
        Debug.Log("Queue Length: " + audioQueue.Count);
        int levelID = SceneManager.GetActiveScene().buildIndex;

        if(levelID != 0)
        {
            if(previousLevelID != levelID) { ChangedLevel(levelID); }

            if(!source.isPlaying && levelID != 1)
            {
                PlayFromQueue();
            }
            else if(!source.isPlaying)
            {
                PlayFromQueue();
            }

            previousLevelID = levelID;
        }
    }

    void ChangedLevel(int id)
    {
        source.Stop();
        
        //Detect MainMenu
        if(id == 1)
        {
            source.Stop();
            if(!source.isPlaying)
            {
                source.clip = clips[0];
            }
            else
            {
                audioQueue.Enqueue(0);
            }
        }
        else if(previousLevelID == 1)
        {
            PlayFromQueue();
        }
        else // if not gone from menu then add song in queue
        {
            QueueRandomSong();
        }

        source.Play();
    }

    void QueueRandomSong()
    {
        if(audioQueue.Count < 1) //If no songs left in queue, add one!
        {
            audioQueue.Enqueue(Random.Range(1,clips.Length-1));
        }
    }

    void PlayFromQueue()
    {
        if(audioQueue.Count < 1) //If no songs left in queue, add one!
        {
            audioQueue.Enqueue(Random.Range(1,clips.Length-1));
        }


        source.Stop();

        source.clip = clips[audioQueue.Dequeue()];

        source.Play();
    }
}
