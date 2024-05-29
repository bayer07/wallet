namespace Wallet.Api.Models
{
    public class MessageModel
    {
        public string Text { get; set; }
        public int Number { get; set; }
        public override string ToString() => $"Text:{Text} Number:{Number}";
    }
}
