using Loan.Models;
using System;
using System.Collections.Generic;

namespace Loan.Business
{
    public abstract class LoanCalculator
    {
        /// <summary>
        /// Calculates the current interest the user has to pay
        /// </summary>
        /// <param name="owedAmmount">The amount owed</param>
        /// <param name="interestRate">Interest rate</param>
        /// <returns></returns>
        protected virtual double CalculateInterest(double owedAmmount, double interestRate)
        {
            return owedAmmount * interestRate / (100 * 12);
        }

        /// <summary>
        /// Calculates the amount the user will pay back on the loan each month
        /// </summary>
        /// <param name="borrowedAmmount">The amount borrowed</param>
        /// <param name="numberOfYears">The number of years to pay back</param>
        /// <returns></returns>
        protected abstract double CalculateTermDeduction(double borrowedAmmount, int numberOfYears);

        /// <summary>
        /// Creates a payment plan for the user
        /// </summary>
        /// <param name="borrowedAmmount">The amount borrowed</param>
        /// <param name="numberOfYears">The number of years to pay it back</param>
        /// <param name="interestRate">The interest rate for the loan</param>
        /// <returns></returns>
        public abstract IEnumerable<PaymentPlanModel> CalculatePaymentPlan(double borrowedAmmount, int numberOfYears, double interestRate);
    }
}
