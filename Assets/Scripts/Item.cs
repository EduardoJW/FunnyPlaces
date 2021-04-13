using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum TipoItem{
        Manteiga,
        Confeito,
        Granulado,
        Espatula,
        Vela,
        Bexiga,
        Confete,
    }

    public TipoItem tipoItem;
    public int quantidade;
}
