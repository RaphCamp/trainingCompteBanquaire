namespace ApplicationBancaireTest.Doubles
{
    public class CompteBancaireStubDao : ICompteBancaireDao
    {
        private Dictionary<Guid, int> comptes;

        public CompteBancaireStubDao(Dictionary<Guid, int> comptes)
        {
            this.comptes = comptes;
        }

        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }


        public void Modification(Guid numeroDeCompte, int argentRetire)
        {
        }

        public int TrouverSolde(Guid numeroDuCompte)
        {

            return comptes[numeroDuCompte];
        }

        List<Transaction> ICompteBancaireDao.GetTransactions(Guid numeroDeCompte)
        {
            throw new NotImplementedException();
        }
    }
}