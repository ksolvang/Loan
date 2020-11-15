using Loan.Models;
using System.Collections.Generic;

namespace Loan.Business
{
    public class HomeLoan : BaseLoan
    {
        private const double interestRate = 3.5;
        public HomeLoan(LoanCalculator loanCalculator) : base(loanCalculator) { }

        /// <summary>
        /// Calculates a payment plan for the user, using the provided LoanCalculator
        /// </summary>
        /// <param name="borrowedAmount">The amount borrowed</param>
        /// <param name="numberOfYears">The number of years used to pay back</param>
        /// <returns></returns>
        public override IEnumerable<PaymentPlanModel> CalculatePaymentPlan(double borrowedAmount, int numberOfYears)
        {
            return LoanCalculator.CalculatePaymentPlan(borrowedAmount, numberOfYears, interestRate);
        }
    }
}
