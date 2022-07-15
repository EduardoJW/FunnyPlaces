using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameAnalytics : NetworkBehaviour
{
    public float M1_DistânciaTotalPercorrida = 0;
    public float M2_TotalDeDinheiroGasto = 0;
    public List<float> M3_TempoParaDigitarASenha = new List<float>();
    public List<float> M4_TempoParaEncontrarJogadoresNoTutorial = new List<float>();
    public float M5_DistânciaPercorridaDuranteOTutorial = 0;
    public List<int> M6_DificuldadeDosItensDaLista = new List<int>();
    public int M7_QuantidadeDeVezesQueTentouSeHidratar = 0;
    public int M8_QuantidadeDeVezesQueSeHidratou = 0;
    public List<float> M9_ValorDaBarraDeEnergiaQuandoSeHidratou = new List<float>();
    public int M10_AcionamentosDoBotaoDeInteracaoSocial = 0;
    public int M11_AcionamentosDoBotaoDeDoacaoDeCupom = 0;
    public int M12_AcionamentosDoBotaoDeJogadorMaisProximo = 0;
    public List<bool> M13_RecuperacaoDeUmItemPerdido = new List<bool>();
    public List<int> M14_TamanhoDaSenhaDigitada = new List<int>();
    public List<float> M15_RazaoEntreValorDaCompraEValorDosItens = new List<float>();
    public int M16_QuantidadeDeVezesQueAEnergiaSeEsgotou = 0;
    public int M17_OrdemEmQueAdquiriuOItemDePrioridade= 0;
    public bool M18_ObteveItemDeCombinacao = false;
    public int M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel = 0;
    public int M20_QuantidadeDeAnimacoesAcionadas = 0;
    public float M21_CreditosAoFinalDaPartida= 2000.0f;
    public bool M22_OpcaoDeLevarOsAmigosAoSobrevoo = false;
    public int M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens = 0;
    public List<int> M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem = new List<int>();
    public int M25_QuantidadeDeVezesQueInteragiu = 0;

    public class Analytics 
    {
        public float M1_DistânciaTotalPercorrida;
        public float M2_TotalDeDinheiroGasto;
        public List<float> M3_TempoParaDigitarASenha = new List<float>();
        public List<float> M4_TempoParaEncontrarJogadoresNoTutorial = new List<float>();
        public float M5_DistânciaPercorridaDuranteOTutorial;
        public List<int> M6_DificuldadeDosItensDaLista = new List<int>();
        public int M7_QuantidadeDeVezesQueTentouSeHidratar;
        public int M8_QuantidadeDeVezesQueSeHidratou;
        public List<float> M9_ValorDaBarraDeEnergiaQuandoSeHidratou = new List<float>();
        public int M10_AcionamentosDoBotaoDeInteracaoSocial;
        public int M11_AcionamentosDoBotaoDeDoacaoDeCupom;
        public int M12_AcionamentosDoBotaoDeJogadorMaisProximo;
        public List<bool> M13_RecuperacaoDeUmItemPerdido = new List<bool>();
        public List<int> M14_TamanhoDaSenhaDigitada = new List<int>();
        public List<float> M15_RazaoEntreValorDaCompraEValorDosItens = new List<float>();
        public int M16_QuantidadeDeVezesQueAEnergiaSeEsgotou;
        public int M17_OrdemEmQueAdquiriuOItemDePrioridade;
        public bool M18_ObteveItemDeCombinacao;
        public int M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel;
        public int M20_QuantidadeDeAnimacoesAcionadas;
        public float M21_CreditosAoFinalDaPartida;
        public bool M22_OpcaoDeLevarOsAmigosAoSobrevoo;
        public int M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens;
        public List<int> M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem = new List<int>();
        public int M25_QuantidadeDeVezesQueInteragiu;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P)) 
        {
            SaveAll();
        }*/
        if (Input.GetKeyDown(KeyCode.L))
        {
            AllSaveAll();
        }
    }


    /*
    [Command]
    public void SaveAll()
    {
        string all = @"{ ""PlayerData"" : [ {";
        string m1 = M1_DistânciaTotalPercorrida.ToString();
        string m2 = M2_TotalDeDinheiroGasto.ToString();
        string m7 = M7_QuantidadeDeVezesQueTentouSeHidratar.ToString();
        string m8 = M8_QuantidadeDeVezesQueSeHidratou.ToString();
        string m9 = ListToString(M9_ValorDaBarraDeEnergiaQuandoSeHidratou);
        all += (@"""DistanciaTotalPercorrida"":"+m1+","+ @"""TotalDinheiroGasto"":" + m2+"," + @"""QuantidadeDeVezesQueTentouSeHidratar"":" + m7 +"," + @"""QuantidadeDeVezesQueSeHidratou"":" + m8 +"," + @"""ValorDaBarraDeEnergiaQuandoSeHidratou"":" + m9);
        all += "} ]}";
        string path = "D:/GameDevGeral/FunnyPlacesMatchRecord/MatchAnalytics0.txt";
        string basepath = "D:/GameDevGeral/FunnyPlacesMatchRecord/MatchAnalytics";
        int numfile = 0;
        string pathnum;
        while (System.IO.File.Exists(path))
        {
            numfile++;
            pathnum = numfile.ToString();
            path = basepath + pathnum + ".txt";
        }
        System.IO.File.WriteAllText(path, all);
    }*/

    public void AllSaveAll() 
    {
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gameobject in arrayGO)
        {
            NetworkIdentity nId = gameobject.GetComponent<NetworkIdentity>();
            ClientSaveAll(nId.connectionToClient);
        }
        
    }

    [TargetRpc]
    public void ClientSaveAll(NetworkConnection client) 
    {
        SaveAll();
    }

    [Command]
    public void SaveAll() 
    {
        Analytics analytics = new Analytics();
        analytics.M1_DistânciaTotalPercorrida = M1_DistânciaTotalPercorrida;
        analytics.M2_TotalDeDinheiroGasto = M2_TotalDeDinheiroGasto;
        analytics.M3_TempoParaDigitarASenha = M3_TempoParaDigitarASenha;
        analytics.M4_TempoParaEncontrarJogadoresNoTutorial = M4_TempoParaEncontrarJogadoresNoTutorial;
        analytics.M5_DistânciaPercorridaDuranteOTutorial = M5_DistânciaPercorridaDuranteOTutorial;
        analytics.M6_DificuldadeDosItensDaLista = M6_DificuldadeDosItensDaLista;
        analytics.M7_QuantidadeDeVezesQueTentouSeHidratar = M7_QuantidadeDeVezesQueTentouSeHidratar;
        analytics.M8_QuantidadeDeVezesQueSeHidratou = M8_QuantidadeDeVezesQueSeHidratou;
        analytics.M9_ValorDaBarraDeEnergiaQuandoSeHidratou = M9_ValorDaBarraDeEnergiaQuandoSeHidratou;
        analytics.M10_AcionamentosDoBotaoDeInteracaoSocial = M10_AcionamentosDoBotaoDeInteracaoSocial;
        analytics.M11_AcionamentosDoBotaoDeDoacaoDeCupom = M11_AcionamentosDoBotaoDeDoacaoDeCupom;
        analytics.M12_AcionamentosDoBotaoDeJogadorMaisProximo = M12_AcionamentosDoBotaoDeJogadorMaisProximo;
        analytics.M13_RecuperacaoDeUmItemPerdido = M13_RecuperacaoDeUmItemPerdido;
        analytics.M14_TamanhoDaSenhaDigitada = M14_TamanhoDaSenhaDigitada;
        analytics.M15_RazaoEntreValorDaCompraEValorDosItens = M15_RazaoEntreValorDaCompraEValorDosItens;
        analytics.M16_QuantidadeDeVezesQueAEnergiaSeEsgotou = M16_QuantidadeDeVezesQueAEnergiaSeEsgotou;
        analytics.M17_OrdemEmQueAdquiriuOItemDePrioridade = M17_OrdemEmQueAdquiriuOItemDePrioridade;
        analytics.M18_ObteveItemDeCombinacao = M18_ObteveItemDeCombinacao;
        analytics.M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel = M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel;
        analytics.M20_QuantidadeDeAnimacoesAcionadas = M20_QuantidadeDeAnimacoesAcionadas;
        analytics.M21_CreditosAoFinalDaPartida = M21_CreditosAoFinalDaPartida;
        analytics.M22_OpcaoDeLevarOsAmigosAoSobrevoo = M22_OpcaoDeLevarOsAmigosAoSobrevoo;
        analytics.M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens = M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens;
        analytics.M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem = M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem;
        analytics.M25_QuantidadeDeVezesQueInteragiu = M25_QuantidadeDeVezesQueInteragiu;

        string json = JsonUtility.ToJson(analytics);
        
        if (!System.IO.Directory.Exists(Application.persistentDataPath+"/FunnyPlacesMatchRecord")) 
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/FunnyPlacesMatchRecord");
        }
        string path = Application.persistentDataPath + "/FunnyPlacesMatchRecord/MatchAnalytics0.json";
        string basepath = Application.persistentDataPath + "/FunnyPlacesMatchRecord/MatchAnalytics";
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

}
