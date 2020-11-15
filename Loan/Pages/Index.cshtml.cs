using Loan.Business;
using Loan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Loan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SeriesLoanCalculator _seriesLoanCalculator;

        [BindProperty, Range(1000,10000000)]
        public double Amount { get; set; }

        [BindProperty, Range(1,30)]
        public int NumberOfYears { get; set; }
        public IEnumerable<PaymentPlanModel> Payments { get; set; }


        public IndexModel(ILogger<IndexModel> logger, SeriesLoanCalculator seriesLoanCalculator)
        {
            _logger = logger;
            _seriesLoanCalculator = seriesLoanCalculator;
        }

        public void OnGet() { }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HomeLoan homeLoan = new HomeLoan(_seriesLoanCalculator);
                    Payments = homeLoan.CalculatePaymentPlan(Amount, NumberOfYears);
                }
                catch (Exception e)
                {
                    //writing error to log, otherwise ignoring them, the end user gets a message through model validation
                    _logger.LogWarning("Something went wrong", e);
                }
            }
        }
    }
}
