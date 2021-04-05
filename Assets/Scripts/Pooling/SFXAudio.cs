using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : PoolItem
{
    [SerializeField] private AudioSource _source;

    public void PlaySFX(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
        StartCoroutine(EndClip(clip.length));
    }

    private IEnumerator EndClip(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _source.Stop();
        _source.clip = null;
        Remove();
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        
    }

    public override void Remove()
    {
        _callback?.Invoke(this);
        gameObject.SetActive(false);
    }

}
