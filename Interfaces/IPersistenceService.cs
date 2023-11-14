using MiniBankApp2.Models;

namespace MiniBankApp2.Interfaces
{
    internal interface IPersistenceService
    {
        public List<Account> FetchAllAccounts();

        public Account FindAccount(int accountNumber);

        public bool SaveAllAccounts(List<Account> accounts);
    }
}
