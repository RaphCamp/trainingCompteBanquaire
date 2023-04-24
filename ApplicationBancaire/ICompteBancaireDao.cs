namespace ApplicationBancaire
{
    public interface ICompteBancaireDao
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactions(Guid numeroDeCompte);
        void Modification(Guid numeroDeCompte, int argentRetire);
        
        public int TrouverSolde(Guid numeroDuCompte);


    }
}