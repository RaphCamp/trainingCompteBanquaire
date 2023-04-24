namespace ApplicationBancaire
{
    public class Transaction
    {
        public Transaction(int balance, int montantTransaction, DateTime date)
        {
            Balance = balance;
            Montant = montantTransaction;
            Date = date;
        }

        public DateTime Date { get; internal set; }
        public int Montant { get; internal set; }
        public int Balance { get; internal set; }
    }
}