using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using LogicLayer.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Invoices.Helper_Service
{
    public class InvoiceServiceHelper
    {
        private readonly IInvoiceRepository _invoiceRepo;

        public InvoiceServiceHelper(IInvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        public async Task<InvoiceType> GetInvoiceTypeByIdAsync(int InvoiceId)
        {
            Invoice? Invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);
            if (Invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }
            return Invoice.InvoiceType;
        }
    }
}
