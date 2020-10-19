namespace JogoDaVelha.Dominio.Modelo
{
    public class MsgResponse
    {
        public MsgResponse(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; private set; }
    }
}