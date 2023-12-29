using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ObjectSwitcher : MonoBehaviour
{
    public GameObject camFPS;
    public GameObject camTPS;
    public GameObject HUd;
    public float zoomSpeed = 5f;
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollWheel) > 0.01f)
        {
            float newFieldOfView = Camera.main.fieldOfView - scrollWheel * zoomSpeed;
            newFieldOfView = Mathf.Clamp(newFieldOfView, 1f, 179f);

            Camera.main.fieldOfView = newFieldOfView;
        }
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeView();
        }
    }
    public void changeHud()
    {
        HUd.SetActive(!HUd.activeSelf);
    }
    public void changeView()
    {
       
            camFPS.SetActive(!camFPS.activeSelf);
            camTPS.SetActive(!camTPS.activeSelf);        
    }
    public void retourne()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
