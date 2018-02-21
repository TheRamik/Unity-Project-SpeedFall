using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;
    [SerializeField] GameObject MoveToNextScene;
    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public bool shaketrue = false;

    GameObject[] platformArray;
    Vector3 originalPos;
    float originalShakeDuration; //<--add this

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        platformArray = GameObject.FindGameObjectsWithTag("platform");
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalShakeDuration = shakeDuration; //<--add this
    }

    void Update()
    {
        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = originalShakeDuration; //<--add this
                camTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (platformArray.Length > 0)
        {
            for (int j = 0; j < platformArray.Length; j++)
            {
                GameObject platform = platformArray[j];
                StartCoroutine(Deactivateplatform(platform, 0.5f));
            }
        }
    }

    public void shakecamera()
    {
        shaketrue = true;
        gameObject.GetComponent<CameraMove>().enabled = false;
    }

    private IEnumerator Deactivateplatform(GameObject platform, float duration)
    {
        string platName = platform.name;
        string temp = platName.Substring(platName.Length - 7);
        if (!(temp == "(clone)"))
        {
            yield return new WaitForSeconds(duration * 10f);
        }
        yield return new WaitForSeconds(duration);
        platform.SetActive(false);
        MoveToNextScene.SetActive(true);
    }
}
