using Loan.Models;
using System.Collections.Generic;

namespace Loan.Business
{
    public abstract class BaseLoan
    {
        protected LoanCalculator LoanCalculator { get; private set; }

        public BaseLoan(LoanCalculator loanCalculator)
        {
            LoanCalculator = loanCalculator;
        }

        /// <summary>
        /// Calculates the loan for the user with the provided loan calculator
        /// </summary>
        /// <param name="borrowedAmount">The amount borrowed</param>
        /// <param name="numberOfYears">The number of years used to pay back</param>
        /// <returns></returns>
        public abstract IEnumerable<PaymentPlanModel> CalculatePaymentPlan(double borrowedAmount, int numberOfYears);
    }
}
