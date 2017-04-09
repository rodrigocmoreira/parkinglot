namespace Vp_himineu.Contratos
{
    using System.Collections.Generic;

    public interface IComando
    {
        string Nome { get; }

        IDictionary<string, string> Parametros { get; }
    }
}
