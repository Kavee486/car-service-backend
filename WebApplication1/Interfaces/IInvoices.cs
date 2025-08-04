using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IInvoices
    {
        Response GetAllInvoices();
        Response GetInvoicesByInvoiceID(string InvoiceID);
        Response PutInvoicesDetails(GetInvoicesModal addInvoice);
        Response DeleteInvoicesDetails(GetInvoicesModal addInvoice);



    }
}
