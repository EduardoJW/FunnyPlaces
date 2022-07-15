using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

/// <summary>
/// This component works in conjunction with the NetworkRoomManagerExtTest to make up the multiplayer room system.
/// <para>The RoomPrefab object of the NetworkRoomManagerExtTest must have this component on it. This component holds basic room player data required for the room to function. Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayerExtTest.</para>
/// </summary>
[DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkRoomPlayerExtTest")]
    [HelpURL("https://mirror-networking.com/docs/Components/NetworkRoomPlayerExtTest.html")]
    public class NetworkRoomPlayerExtTest : NetworkBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayerExtTest));

        /// <summary>
        /// This flag controls whether the default UI is shown for the room player.
        /// <para>As this UI is rendered using the old GUI system, it is only recommended for testing purposes.</para>
        /// </summary>
        [Tooltip("This flag controls whether the default UI is shown for the room player")]
        public bool showRoomGUI = true;

        [Header("Diagnostics")]

        /// <summary>
        /// Diagnostic flag indicating whether this player is ready for the game to begin.
        /// <para>Invoke CmdChangeReadyState method on the client to set this flag.</para>
        /// <para>When all players are ready to begin, the game will start. This should not be set directly, CmdChangeReadyState should be called on the client to set it on the server.</para>
        /// </summary>
        [Tooltip("Diagnostic flag indicating whether this player is ready for the game to begin")]
        [SyncVar(hook = nameof(ReadyStateChanged))]
        public bool readyToBegin;

        /// <summary>
        /// Diagnostic index of the player, e.g. Player1, Player2, etc.
        /// </summary>
        [Tooltip("Diagnostic index of the player, e.g. Player1, Player2, etc.")]
        [SyncVar(hook = nameof(IndexChanged))]
        public int index;

    public int A1 = 0;
    public int A2 = 0;
    public int A3 = 0;
    public int A4 = 0;
    public int A5 = 0;
    public int A6 = 0;
    public int A7 = 0;
    public int A8 = 0;
    public int A9 = 0;
    public int A10 = 0;
    public int A11 = 0;
    public int A12 = 0;
    public int A13 = 0;
    public int A14 = 0;
    public int A15 = 0;

    public GameObject Questionnaire;

    public class Answers
    {
        public int A1;
        public int A2;
        public int A3;
        public int A4;
        public int A5;
        public int A6;
        public int A7;
        public int A8;
        public int A9;
        public int A10;
        public int A11;
        public int A12;
        public int A13;
        public int A14;
        public int A15;
    }

    public void RecordAnswers1()
    {
        A1 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta1/RespostaPergunta1").gameObject.GetComponent<TMP_Dropdown>().value;
        A2 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta2/RespostaPergunta2").gameObject.GetComponent<TMP_Dropdown>().value;
        A3 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta3/RespostaPergunta3").gameObject.GetComponent<TMP_Dropdown>().value;
        A4 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta4/RespostaPergunta4").gameObject.GetComponent<TMP_Dropdown>().value;
        A5 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta5/RespostaPergunta5").gameObject.GetComponent<TMP_Dropdown>().value;
    }
    public void RecordAnswers2()
    {
        A6 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta6/RespostaPergunta6").gameObject.GetComponent<TMP_Dropdown>().value;
        A7 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta7/RespostaPergunta7").gameObject.GetComponent<TMP_Dropdown>().value;
        A8 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta8/RespostaPergunta8").gameObject.GetComponent<TMP_Dropdown>().value;
        A9 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta9/RespostaPergunta9").gameObject.GetComponent<TMP_Dropdown>().value;
        A10 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta10/RespostaPergunta10").gameObject.GetComponent<TMP_Dropdown>().value;
    }
    public void RecordAnswers3()
    {
        A11 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta11/RespostaPergunta11").gameObject.GetComponent<TMP_Dropdown>().value;
        A12 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta12/RespostaPergunta12").gameObject.GetComponent<TMP_Dropdown>().value;
        A13 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta13/RespostaPergunta13").gameObject.GetComponent<TMP_Dropdown>().value;
        A14 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta14/RespostaPergunta14").gameObject.GetComponent<TMP_Dropdown>().value;
        A15 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta15/RespostaPergunta15").gameObject.GetComponent<TMP_Dropdown>().value;
    }

   
    public void SaveQuestionnaire() 
    {
        SaveQuestionnaireToServer(A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12, A13, A14, A15);
    }

    [Command]
    public void SaveQuestionnaireToServer(int A1, int A2, int A3, int A4, int A5, int A6, int A7, int A8, int A9, int A10, int A11, int A12, int A13, int A14, int A15)
    {
        Answers answer = new Answers();
        answer.A1 = A1;
        answer.A2 = A2;
        answer.A3 = A3;
        answer.A4 = A4;
        answer.A5 = A5;
        answer.A6 = A6;
        answer.A7 = A7;
        answer.A8 = A8;
        answer.A9 = A9;
        answer.A10 = A10;
        answer.A11 = A11;
        answer.A12 = A12;
        answer.A13 = A13;
        answer.A14 = A14;
        answer.A15 = A15;

        string json = JsonUtility.ToJson(answer);
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/FunnyPlacesQuestionnaire"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/FunnyPlacesQuestionnaire");
        }
        string path = Application.persistentDataPath + "/FunnyPlacesQuestionnaire/QuestionnaireAnswer0.json";
        string basepath = Application.persistentDataPath + "/FunnyPlacesQuestionnaire/QuestionnaireAnswer";
        int numfile = 0;
        string pathnum;
        while (System.IO.File.Exists(path))
        {
            numfile++;
            pathnum = numfile.ToString();
            path = basepath + pathnum + ".json";
        }
        System.IO.File.WriteAllText(path, json);
    }

    #region Unity Callbacks

    /// <summary>
    /// Do not use Start - Override OnStartrHost / OnStartClient instead!
    /// </summary>
    public void Start()
        {
            if (NetworkManager.singleton is NetworkRoomManagerExtTest room)
            {
                // NetworkRoomPlayerExtTest object must be set to DontDestroyOnLoad along with NetworkRoomManagerExtTest
                // in server and all clients, otherwise it will be respawned in the game scene which would
                // have undesireable effects.
                if (room.dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);

                room.roomSlots.Add(this);

                if (NetworkServer.active)
                    room.RecalculateRoomPlayerIndices();

                if (NetworkClient.active)
                    room.CallOnClientEnterRoom();
            }
            else
                logger.LogError("RoomPlayer could not find a NetworkRoomManagerExtTest. The RoomPlayer requires a NetworkRoomManagerExtTest object to function. Make sure that there is one in the scene.");
        }

        public virtual void OnDisable()
        {
            if (NetworkClient.active && NetworkManager.singleton is NetworkRoomManagerExtTest room)
            {
                // only need to call this on client as server removes it before object is destroyed
                room.roomSlots.Remove(this);

                room.CallOnClientExitRoom();
            }
        }

        #endregion

        #region Commands

        [Command]
        public void CmdChangeReadyState(bool readyState)
        {
            readyToBegin = readyState;
            NetworkRoomManagerExtTest room = NetworkManager.singleton as NetworkRoomManagerExtTest;
            if (room != null)
            {
                room.ReadyStatusChanged();
            }
        }

        #endregion

        #region SyncVar Hooks

        /// <summary>
        /// This is a hook that is invoked on clients when the index changes.
        /// </summary>
        /// <param name="oldIndex">The old index value</param>
        /// <param name="newIndex">The new index value</param>
        public virtual void IndexChanged(int oldIndex, int newIndex) { }

        /// <summary>
        /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
        /// <para>This function is called when the a client player calls CmdChangeReadyState.</para>
        /// </summary>
        /// <param name="newReadyState">New Ready State</param>
        public virtual void ReadyStateChanged(bool oldReadyState, bool newReadyState) { }

        #endregion

        #region Room Client Virtuals

        /// <summary>
        /// This is a hook that is invoked on clients for all room player objects when entering the room.
        /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
        /// </summary>
        public virtual void OnClientEnterRoom() { }

        /// <summary>
        /// This is a hook that is invoked on clients for all room player objects when exiting the room.
        /// </summary>
        public virtual void OnClientExitRoom() { }

        #endregion

        #region Optional UI

        /// <summary>
        /// Render a UI for the room.   Override to provide your on UI
        /// </summary>
        public virtual void OnGUI()
        {
            if (!showRoomGUI)
                return;

            NetworkRoomManagerExtTest room = NetworkManager.singleton as NetworkRoomManagerExtTest;
            if (room)
            {
                if (!room.showRoomGUI)
                    return;

                if (!NetworkManager.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {
            GUILayout.BeginArea(new Rect(20f + (index * 100), 200f, 90f, 130f));

            GUILayout.Label($"Player [{index + 1}]");

            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }

        #endregion
    }
