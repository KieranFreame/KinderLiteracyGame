using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksHandler : MonoBehaviour
{
    readonly List<ParticleSystem> fireworks = new();

    private void OnEnable()
    {
        if (fireworks.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                fireworks.Add(transform.GetChild(i).GetComponent<ParticleSystem>());
            }
        }
    }

    public IEnumerator PlayFireworks()
    {
        foreach (ParticleSystem f in fireworks)
        {
            f.Play();
            yield return new WaitForSeconds(1f);
        }
    }

    public void StopFireworks()
    {
        foreach (ParticleSystem f in fireworks)
        {
            f.Stop();
        }
    }
}
