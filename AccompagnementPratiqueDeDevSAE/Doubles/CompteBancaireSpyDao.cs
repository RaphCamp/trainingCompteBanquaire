namespace ApplicationBancaireTest.Doubles
{
    internal class CompteBancaireSpyDao : ICompteBancaireDao
    {
        private Dictionary<Guid, int> comptes;
        private List<Transaction> transactions = new();

        public CompteBancaireSpyDao(Dictionary<Guid, int> comptes)
        {
            this.comptes = comptes;
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public List<Transaction> GetTransactions(Guid numeroDeCompte)
        {
            return this.transactions;
        }

        public void Modification(Guid numeroDeCompte, int nouveauSolde)
        {
            comptes[numeroDeCompte] = nouveauSolde;
        }

        public int TrouverSolde(Guid numeroDuCompte)
        {
            return comptes[numeroDuCompte];
        }

        internal int GetNouveauSolde(Guid numeroDeCompte)
        {
            return comptes[numeroDeCompte];
        }
    }
}