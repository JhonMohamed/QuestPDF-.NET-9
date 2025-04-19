using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> ObtenerFacturasAsync();
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
            => _context = context;

        public async Task<List<Invoice>> ObtenerFacturasAsync()
        {
            return await _context.Invoices
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }
    }
}