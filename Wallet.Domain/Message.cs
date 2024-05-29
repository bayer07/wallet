namespace Wallet.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }

        public override string ToString() => $"Id:{Id} Text:{Text} Date:{Date} Number:{Number}";
    }
}
