using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameAnalytics : NetworkBehaviour
{
    public float M1_DistânciaTotalPercorrida = 0;
    public float M2_TotalDeDinheiroGasto = 0;
    public float[] M3_TempoParaDigitarASenha;
    public float[] M4_TempoParaEncontrarJogadoresNoTutorial;
    public float M5_DistânciaPercorridaDuranteOTutorial = 0;
    public int[] M6_DificuldadeDosItensDaLista;
    public int M7_QuantidadeDeVezesQueTentouSeHidratar = 0;
    public int M8_QuantidadeDeVezesQueSeHidratou = 0;
    public List<string> M9_ValorDaBarraDeEnergiaQuandoSeHidratou = new List<string>();
    public int M10_AcionamentosDoBotaoDeInteracaoSocial = 0;
    public int M11_AcionamentosDoBotaoDeDoacaoDeCupom = 0;
    public int M12_AcionamentosDoBotaoDeJogadorMaisProximo = 0;
    public bool[] M13_RecuperacaoDeUmItemPerdido;
    public int[] M14_TamanhoDaSenhaDigitada;
    public float[] M15_RazaoEntreValorDaCompraEValorDosItens;
    public int M16_QuantidadeDeVezesQueAEnergiaSeEsgotou = 0;
    public int M17_OrdemEmQueAdquiriuOItemDePrioridade;
    public bool M18_ObteveItemDeCombinacao;
    public int M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel = 0;
    public int M20_QuantidadeDeAnimacoesAcionadas = 0;
    public float M21_CreditosAoFinalDaPartida= 2000.0f;
    public bool M22_OpcaoDeLevarOsAmigosAoSobrevoo = false;
    public int M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens =0;
    public int[] M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem;
    public int M25_QuantidadeDeVezesQueInteragiu;

    public class Analytics 
    {
        public float M1_DistânciaTotalPercorrida;
        public float M2_TotalDeDinheiroGasto;
        public float[] M3_TempoParaDigitarASenha;
        public float[] M4_TempoParaEncontrarJogadoresNoTutorial;
        public float M5_DistânciaPercorridaDuranteOTutorial;
        public int[] M6_DificuldadeDosItensDaLista;
        public int M7_QuantidadeDeVezesQueTentouSeHidratar;
        public int M8_QuantidadeDeVezesQueSeHidratou;
        public List<string> M9_ValorDaBarraDeEnergiaQuandoSeHidratou = new List<string>();
        public int M10_AcionamentosDoBotaoDeInteracaoSocial;
        public int M11_AcionamentosDoBotaoDeDoacaoDeCupom;
        public int M12_AcionamentosDoBotaoDeJogadorMaisProximo;
        public bool[] M13_RecuperacaoDeUmItemPerdido;
        public int[] M14_TamanhoDaSenhaDigitada;
        public float[] M15_RazaoEntreValorDaCompraEValorDosItens;
        public int M16_QuantidadeDeVezesQueAEnergiaSeEsgotou;
        public int M17_OrdemEmQueAdquiriuOItemDePrioridade;
        public bool M18_ObteveItemDeCombinacao;
        public int M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel;
        public int M20_QuantidadeDeAnimacoesAcionadas;
        public float M21_CreditosAoFinalDaPartida;
        public bool M22_OpcaoDeLevarOsAmigosAoSobrevoo;
        public int M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens;
        public int[] M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem;
        public int M25_QuantidadeDeVezesQueInteragiu;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            SaveAll();
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

        string path = "D:/GameDevGeral/FunnyPlacesMatchRecord/MatchAnalytics0.json";
        string basepath = "D:/GameDevGeral/FunnyPlacesMatchRecord/MatchAnalytics";
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
    

    private string ListToString(List<string> list) 
    {
        //string delim = ",";
        //string str = System.String.Join(delim, list);
        string str = "[";
        string current = "";
        foreach (var i in list) 
        {
            current = i.ToString();
            str += @"""" + current + @""",";
        }
        str = str.Remove(str.Length - 1);
        str += "]";
        return str;
    }

    /*
        [Command]
        public void CmdUpdateDistanciaTotalPercorrida(float trecho){
            M1_DistânciaTotalPercorrida+=trecho;
        }

        [Command]
        public void CmdUpdateDinheiroTotalGasto(float valor){

        }

        [Command]
        public void CmdUpdateTempoParaDigitarASenha(float valor){

        }

        [Command]
        public void CmdUpdateQuantidadeDeVezesQueTentouSeHidratar (){
            M7_QuantidadeDeVezesQueTentouSeHidratar++;
            Debug.Log("Chegou Comando");
        }

        [Command]
        public void CmdUpdateQuantidadeDeVezesQueSeHidratou (){

        }

        [Command]
        public void CmdUpdateAcionamentosDoBotaoDeInteracaoSocial (){
            M10_AcionamentosDoBotaoDeInteracaoSocial++;
        }

        [Command]
        public void CmdUpdateAcionamentosDoBotaoDeDoacaoDeCupom(){
            M11_AcionamentosDoBotaoDeDoacaoDeCupom++;
        }

        [Command]
        public void CmdUpdateAcionamentosDoBotaoDeBuscaDeJogadorMaisProximo(){
            M12_AcionamentosDoBotaoDeJogadorMaisProximo++;
        }

        [Command]
        public void CmdUpdateQuantidadeDeVezesEmQueAEgnergiaSeEsgotou(){
            M16_QuantidadeDeVezesQueAEnergiaSeEsgotou++;
        }

        [Command]
        public void CmdUpdateQuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel(){
            M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel++;
        }

        [Command]
        public void CmdUpdateQuantidadeDeAnimacoesAcionadas(){
            M20_QuantidadeDeAnimacoesAcionadas++;
        }

        [Command]
        public void CmdUpdateQuantidadeDeAcionamentosDaListaDeItens(){
            M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens++;
        }

        [Command]
        public void CmdUpdateQuantidadeDeAcionamentosDaDicaDeItens(){

        }
        */

}
