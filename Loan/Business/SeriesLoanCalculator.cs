using Loan.Models;
using System;
using System.Collections.Generic;

namespace Loan.Business
{
    public class SeriesLoanCalculator : LoanCalculator
    {
        /// <summary>
        /// Creates a payment plan for a series loan.
        /// </summary>
        /// <param name="borrowedAmmount">The amount the loan is for</param>
        /// <param name="numberOfYears">Number of years the user wishes to spend paying back the loan</param>
        /// <param name="interestRate">The interest rate for the loan</param>
        /// <returns>A list as a payment plan. The first term will be empty except for the borrowed amount</returns>
        public override IEnumerable<PaymentPlanModel> CalculatePaymentPlan(double borrowedAmmount, int numberOfYears, double interestRate)
        {
            var model = new List<PaymentPlanModel>();
            model.Add(new PaymentPlanModel()
            {
                OutstandingDebt = borrowedAmmount
            });
            var termPayment = CalculateTermDeduction(borrowedAmmount, numberOfYears);

            for (int i = 0; i < numberOfYears * 12; i++)
            {
                var interest = CalculateInterest(borrowedAmmount, interestRate);
                if (borrowedAmmount < termPayment)
                {
                    termPayment = borrowedAmmount;
                }
                borrowedAmmount -= termPayment;
                var payment = new PaymentPlanModel()
                {
                    Term = i + 1,
                    TermTotalAmount = termPayment + interest,
                    Interest = interest,
                    Principal = termPayment,
                    OutstandingDebt = borrowedAmmount
                };
                model.Add(payment);
            }

            return model;
        }

        /// <summary>
        /// Calculates the amount the user will pay back on the loan each month
        /// </summary>
        /// <param name="borrowedAmmount">The amount borrowed</param>
        /// <param name="numberOfYears">The number of years to pay back</param>
        /// <returns></returns>
        protected override double CalculateTermDeduction(double borrowedAmmount, int numberOfYears)
        {
            if (numberOfYears < 1)
            {
                numberOfYears = 1;
            }
            if (borrowedAmmount < 1000)
            {
                throw new Exception("You cannot borrow less than 1000");
            }
            return borrowedAmmount / (numberOfYears * 12);
        }
    }
}
