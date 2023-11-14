using MiniBankApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankApp2.Interfaces
{
    internal interface IReportService
    {
        public void PrintAccountHistory(IList<Transaction> transactions);

        public void PrintAccountBeneficiaries(IList<Beneficiary> beneficiaries);

    }
}
