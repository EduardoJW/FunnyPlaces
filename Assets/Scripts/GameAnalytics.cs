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
    public int M7_QuantidadeDeVezesQueSeHidratou=0;
    public float[] M8_ValorDaBarraDeEnergiaQuandoSeHidratou;
    public int M9_AcionamentosDoBotaoDeInteracaoSocial=0;
    public int M10_AcionamentosDoBotaoDeDoacaoDeCupom=0;
    public int M11_AcionamentosDoBotaoDeJogadorMaisProximo=0;
    public bool[] M12_RecuperacaoDeUmItemPerdido;
    public int[] M13_TamanhoDaSenhaDigitada;
    public float[] M14_RazaoEntreValorDaCompraEValorDosItens;
    public int M15_QuantidadeDeVezesQueAEnergiaSeEsgotou=0;
    public int M16_OrdemEmQueAdquiriuOItemDePrioridade;
    public bool M17_ObteveItemDeCombinacao;
    public int M18_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel=0;
    public int M19_QuantidadeDeAnimacoesAcionadas=0;
    public float M20_CreditosAoFinalDaPartida;
    public bool M21_OpcaoDeLevarOsAmigosAoSobrevoo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void UpdateDistanciaTotalPercorrida(float trecho){
        M1_DistânciaTotalPercorrida+=trecho;
    }

    [Command]
    public void UpdateDinheiroTotalGasto(float valor){

    }

    [Command]
    public void UpdateTempoParaDigitarASenha(float valor){

    }

    [Command]
    public void UpdateQuantidadeDeVezesQueSeHidratou (int valor){
        M7_QuantidadeDeVezesQueSeHidratou+=valor;
    }

    [Command]
    public void UpdateAcionamentosDoBotaoDeInteracaoSocial (){
        M9_AcionamentosDoBotaoDeInteracaoSocial++;
    }
    
    [Command]
    public void UpdateAcionamentosDoBotaoDeDoacaoDeCupom(){
        M10_AcionamentosDoBotaoDeDoacaoDeCupom++;
    }

    [Command]
    public void UpdateAcionamentosDoBotaoDeBuscaDeJogadorMaisProximo(){
        M11_AcionamentosDoBotaoDeJogadorMaisProximo++;
    }

    [Command]
    public void UpdateQuantidadeDeVezesEmQueAEgnergiaSeEsgotou(){
        M15_QuantidadeDeVezesQueAEnergiaSeEsgotou++;
    }

    [Command]
    public void UpdateQuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel(){
        M18_QuantidadeDeVezesQueOBotaoDeInteracaoSocialFicouDisponivel++;
    }

    [Command]
    public void UpdatQuantidadeDeAnimacoesAcionadas(){
        M19_QuantidadeDeAnimacoesAcionadas++;
    }

}
