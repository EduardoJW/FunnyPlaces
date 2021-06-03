using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameAnalytics : NetworkBehaviour
{
    public float M1_DistânciaTotalPercorrida=0;
    public float M2_TotalDeDinheiroGasto=0;
    public float[] M3_TempoParaDigitarASenha;
    public float[] M4_TempoParaEncontrarJogadoresNoTutorial;
    public float M5_DistânciaPercorridaDuranteOTutorial=0;
    public int[] M6_DificuldadeDosItensDaLista;
    public int M7_QuantidadeDeVezesQueTentouSeHidratar=0;
    public int M8_QuantidadeDeVezesQueSeHidratou=0;
    public float[] M9_ValorDaBarraDeEnergiaQuandoSeHidratou;
    public int M10_AcionamentosDoBotaoDeInteracaoSocial=0;
    public int M11_AcionamentosDoBotaoDeDoacaoDeCupom=0;
    public int M12_AcionamentosDoBotaoDeJogadorMaisProximo=0;
    public bool[] M13_RecuperacaoDeUmItemPerdido;
    public int[] M14_TamanhoDaSenhaDigitada;
    public float[] M15_RazaoEntreValorDaCompraEValorDosItens;
    public int M16_QuantidadeDeVezesQueAEnergiaSeEsgotou=0;
    public int M17_OrdemEmQueAdquiriuOItemDePrioridade;
    public bool M18_ObteveItemDeCombinacao;
    public int M19_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel=0;
    public int M20_QuantidadeDeAnimacoesAcionadas=0;
    public float M21_CreditosAoFinalDaPartida;
    public bool M22_OpcaoDeLevarOsAmigosAoSobrevoo;
    public int M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens;
    public int[] M24_QuantidadeDeVezesQueAcionouOBotaoDeDicaDeItem;
    public int M25_QuantidadeDeVezesQueInteragiu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
