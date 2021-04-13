using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;



public class HostLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField PasswordInputField = null;
    [SerializeField] private Button HostButton = null;

    /*private void OnEnable(){
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;

    }*/
    

    public void HostLobby(){
        networkManager.StartServer();
    }

    private void HandleClientConnected(){
        HostButton.interactable=true;
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected(){
        HostButton.interactable=true;
    }

    public void ValidatePassword(){
        string password = PasswordInputField.text;
        if (password == "psi-inf"){
            HostLobby();
            //GameObject.Find("ServerManager").transform.Find("ServerCamera").gameObject.SetActive(true);
            //GameObject.Find("ServerManager").transform.Find("ServerControlerUI").gameObject.SetActive(true);
            gameObject.SetActive(false);
        }else{
            Debug.Log("senha incorreta");    
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
