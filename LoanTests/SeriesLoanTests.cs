using Loan.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LoanTests
{
    [TestClass]
    public class SeriesLoanTests : SeriesLoanCalculator
    {
        #region CalculateInterest


        [TestMethod]
        [DataRow(0)]
        public void IfInterestIs0ThenResultIs0(double interest)
        {
            var result = CalculateInterest(1000000, interest);
            Assert.AreEqual(result, 0.00);
        }

        #endregion

        #region CalculateTermDeduction
        [TestMethod]
        public void TermPrincipalShouldBe4166For1000000Over20Years()
        {
            var result = CalculateTermDeduction(1000000, 20);
            Assert.AreEqual(Math.Round(result, 2), 4166.67);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void IfInputIsLessThan1Return1Year(int years)
        {
            var result = CalculateTermDeduction(1000000, years);
            Assert.AreEqual(Math.Round(result, 2), 83333.33);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(999)]
        public void IfAmmountIsLessThan12GetException(float amount)
        {
            Assert.ThrowsException<Exception>(() => CalculateTermDeduction(amount, 1));
        }

        #endregion

        #region CalculatePaymentPlan

        [TestMethod]
        [DataRow(1000000)]
        public void OutstandingDebtInTerm0ShouldBeTheSameAsAmmountToBorrow(double outstandingDebt)
        {
            var loanCalc = new SeriesLoanCalculator();
            var loan = new HomeLoan(loanCalc);
            var a = loan.CalculatePaymentPlan(outstandingDebt, 10);
            Assert.IsTrue(a.First(x => x.Term == 0).OutstandingDebt == outstandingDebt);
        }

        [TestMethod]
        public void TheUserShouldNeverPayMoreThanTheyOwe()
        {
            var loanCalc = new SeriesLoanCalculator();
            var loan = new HomeLoan(loanCalc);
            var a = loan.CalculatePaymentPlan(1000000, 30);
            Assert.IsTrue(!a.Any(x => x.OutstandingDebt < 0));
        }

        [TestMethod]
        public void TotalAmmountInInterests()
        {
            var loanCalc = new SeriesLoanCalculator();
            var loan = new HomeLoan(loanCalc);
            var a = loan.CalculatePaymentPlan(1000000, 20);
            double interests = 0.0;
            foreach ( var payment in a)
            {
                interests += payment.Interest;
            }
            Assert.IsTrue(Math.Round(interests,0) == 351458);
        }

        #endregion
    }
}
