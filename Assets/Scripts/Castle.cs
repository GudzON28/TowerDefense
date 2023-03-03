using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private int healt = 10;
    [SerializeField] private Text healthText;
    [SerializeField] private AudioClip castleDemageSoundFX;
    private AudioSource audioSource;


    private void Start()
    {
        healthText.text = healt.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(int damage)
    {
        audioSource.PlayOneShot(castleDemageSoundFX);
        healt -= damage;
        healthText.text = healt.ToString();
    }
}
