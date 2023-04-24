using ApplicationBancaire;
using ApplicationBancaireTest.Doubles;

namespace AccompagnementPratiqueDeDevSAE
{
    [TestClass]
    public class CompteBancaireTest
    {

        private Dictionary<Guid, int> comptes = new Dictionary<Guid, int>();
        private Guid numeroCompte1 = Guid.NewGuid();
        private Guid numeroCompte2 = Guid.NewGuid();
        private ICompteBancaireDao compteBancaireDao;
        IFournisseurDateTime fournisseurStubDateTime = new FournisseurStubDateTime();

        [TestInitialize]
        public void InitialisationComptes()
        {
            comptes[numeroCompte1] = 100;
            comptes[numeroCompte2] = 200;
            compteBancaireDao = new CompteBancaireStubDao(comptes);
        }

        [TestMethod]
        public void SiSoldeEstEgalA100AlorsObtenirSoldeRetourne100()
        {
            //Arrange
            int soldeAttendu = 100;
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireDao, fournisseurStubDateTime);

            //Act
            int soldeObtenu = compteBancaire.ObtenirSolde();

            //Assert
            Assert.AreEqual(soldeAttendu, soldeObtenu);
        }

        [TestMethod]
        public void SiSoldeEstEgalA200AlorsObtenirSoldeRetourne200()
        {
            //Arrange
            CompteBancaire compteBancaire = new(numeroCompte2, compteBancaireDao, fournisseurStubDateTime);

            //Act
            int soldeObtenu = compteBancaire.ObtenirSolde();

            //Assert
            int soldeAttendu = 200;
            Assert.AreEqual(soldeAttendu, soldeObtenu);
        }

        //[TestMethod]
        //public void SiSoldeEstEgalA300AlorsObtenirSoldeRetourne300()
        //{
        //    //Arrange
        //    int numeroDeCompte = 3;
        //    ICompteBancaireDao compteBancaireDao = new CompteBancaireFakeDao();
        //    CompteBancaire compteBancaire = new(numeroDeCompte, compteBancaireDao);

        //    //Act
        //    int soldeObtenu = compteBancaire.ObtenirSolde();

        //    //Assert
        //    int soldeAttendu = 300;
        //    Assert.AreEqual(soldeAttendu, soldeObtenu);
        //}
        //[TestMethod]
        //public void SiSoldeEstEgalA400AlorsObtenirSoldeRetourne400()
        //{
        //    //Arrange
        //    int numeroDeCompte = 4;
        //    ICompteBancaireDao compteBancaireDao = new CompteBancaireFakeDao();
        //    CompteBancaire compteBancaire = new(numeroDeCompte, compteBancaireDao);

        //    //Act
        //    int soldeObtenu = compteBancaire.ObtenirSolde();

        //    //Assert
        //    int soldeAttendu = 400;
        //    Assert.AreEqual(soldeAttendu, soldeObtenu);
        //}


        [TestMethod]
        public void SiSoldeEstEgalA100QuandJeRetire60AlorsLeNouveauSoldeEstEgalA40()
        {
            //Arrange 
            CompteBancaireSpyDao compteBancaireSpyDao = new CompteBancaireSpyDao(comptes);
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireSpyDao, fournisseurStubDateTime);
            int argentRetire = 60;
            int soldeAttendu = 100 - argentRetire;

            //Act
            compteBancaire.Retrait(argentRetire);

            //Assert
            Assert.AreEqual(soldeAttendu, compteBancaireSpyDao.GetNouveauSolde(numeroCompte1));
        }


        [TestMethod]
        public void SiSoldeEstEgalA100QuandJeDepose60AlorsLeNouveauSoldeEstEgalA160()
        {
            //Arrange 
            CompteBancaireSpyDao compteBancaireSpyDao = new CompteBancaireSpyDao(comptes);
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireSpyDao, fournisseurStubDateTime);
            int argentDepose = 60;
            int soldeAttendu = 100 + argentDepose;

            //Act
            compteBancaire.Depot(argentDepose);

            //Assert
            Assert.AreEqual(soldeAttendu, compteBancaireSpyDao.GetNouveauSolde(numeroCompte1));
            Assert.AreEqual(200, compteBancaireSpyDao.GetNouveauSolde(numeroCompte2));
        }

        [TestMethod]
        public void SiSoldeEgal100QuandJeRetire80AlorsUneTransactionExistePourLeJourJEtBalanceFinaleEgale20()
        {
            //Arrange
            CompteBancaireSpyDao compteBancaireSpyDao = new CompteBancaireSpyDao(comptes);
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireSpyDao, fournisseurStubDateTime);
            int montantDuRetrait = 80;
            int balanceAttendue = 100 - montantDuRetrait;
            compteBancaire.Retrait(montantDuRetrait);

            //Act
            List<Transaction> transactions = compteBancaire.GetTransactions();

            //Assert
            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(montantDuRetrait, transactions[0].Montant);
            Assert.AreEqual(balanceAttendue, transactions[0].Balance);
            Assert.AreEqual(fournisseurStubDateTime.GetNow(), transactions[0].Date);
        }

        [TestMethod]
        public void SiSoldeEgal100QuandJeRetire40Puis40AlorsUneTransactionExistePourLeJourJEtBalanceFinaleEgale20()
        {
            //Arrange
            CompteBancaireSpyDao compteBancaireSpyDao = new CompteBancaireSpyDao(comptes);
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireSpyDao, fournisseurStubDateTime);
            int montantDuRetrait = 40;
            int balanceAttendue1 = 100 - montantDuRetrait;
            int balanceAttendue2 = 100 - montantDuRetrait - montantDuRetrait;
            compteBancaire.Retrait(montantDuRetrait);
            compteBancaire.Retrait(montantDuRetrait);

            //Act
            List<Transaction> transactions = compteBancaire.GetTransactions();

            //Assert
            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual(montantDuRetrait, transactions[0].Montant);
            Assert.AreEqual(balanceAttendue1, transactions[0].Balance);
            Assert.AreEqual(fournisseurStubDateTime.GetNow(), transactions[0].Date);
            Assert.AreEqual(montantDuRetrait, transactions[1].Montant);
            Assert.AreEqual(balanceAttendue2, transactions[1].Balance);
            Assert.AreEqual(fournisseurStubDateTime.GetNow(), transactions[1].Date);
        }        

        [TestMethod]
        public void SiSoldeEgal100QuandJeRetire40PuisDepose50AlorsUneTransactionExistePourLeJourJEtBalanceFinaleEgale110()
        {
            //Arrange
            CompteBancaireSpyDao compteBancaireSpyDao = new CompteBancaireSpyDao(comptes);
            CompteBancaire compteBancaire = new(numeroCompte1, compteBancaireSpyDao, fournisseurStubDateTime);
            int montantDuRetrait = 40;
            int montantDuDepot = 50;
            int balanceAttendue1 = 100 - montantDuRetrait;
            int balanceAttendue2 = balanceAttendue1 + montantDuDepot;
            compteBancaire.Retrait(montantDuRetrait);
            compteBancaire.Depot(montantDuDepot);

            //Act
            List<Transaction> transactions = compteBancaire.GetTransactions();

            //Assert
            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual(montantDuRetrait, transactions[0].Montant);
            Assert.AreEqual(balanceAttendue1, transactions[0].Balance);
            Assert.AreEqual(fournisseurStubDateTime.GetNow(), transactions[0].Date);
            Assert.AreEqual(montantDuDepot, transactions[1].Montant);
            Assert.AreEqual(balanceAttendue2, transactions[1].Balance);
            Assert.AreEqual(fournisseurStubDateTime.GetNow(), transactions[1].Date);
        }
    }
}