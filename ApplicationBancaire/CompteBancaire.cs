namespace ApplicationBancaire
{
    public class CompteBancaire
    {
        private ICompteBancaireDao compteBancaireDao;
        private readonly IFournisseurDateTime fournisseurDateTime;
        private Guid numeroDeCompte;

        public CompteBancaire(Guid numeroDeCompte, ICompteBancaireDao compteBancaireDao,IFournisseurDateTime fournisseurDateTime)
        {
            this.numeroDeCompte = numeroDeCompte;
            this.compteBancaireDao = compteBancaireDao;
            this.fournisseurDateTime = fournisseurDateTime;
        }

        public void Depot(int argentDepose)
        {
            int nouveauSolde = ObtenirSolde() + argentDepose;
            CreateTransaction(nouveauSolde, argentDepose);
            compteBancaireDao.Modification(numeroDeCompte, nouveauSolde);
        }

        public List<Transaction> GetTransactions()
        {
            return compteBancaireDao.GetTransactions(numeroDeCompte);
        }

        public int ObtenirSolde()
        {
            return compteBancaireDao.TrouverSolde(numeroDeCompte);
        }

        public void Retrait(int argentRetire)
        {
            int nouveauSolde = ObtenirSolde() - argentRetire;
            CreateTransaction(nouveauSolde, argentRetire);
            compteBancaireDao.Modification(numeroDeCompte, nouveauSolde);
        }

        private void CreateTransaction(int nouveauSolde,int montantTransaction)
        {
            Transaction transaction = new Transaction(nouveauSolde, montantTransaction, fournisseurDateTime.GetNow());
            compteBancaireDao.AddTransaction(transaction);
        }
    }
}