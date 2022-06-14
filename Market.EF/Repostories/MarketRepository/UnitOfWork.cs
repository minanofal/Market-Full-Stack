using AutoMapper;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.InterFaces.MarketInterfaces.IUnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.MarketRepository
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Categories { get; private set; }
        public ITypeRepository Types { get; private set; }
        public ICompanyRepository Companies { get; private set; }

        public IProductRepository products { get; private set; }

        public UnitOfWork(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            Categories = new CategoryRepository(_context , mapper);
            Types = new TypeRepository(_context , mapper);
            Companies = new CompanyRepository(_context, mapper);
            products = new ProductRepository(_context, mapper);
        }



        public void Dispose()
        {
           
        }
    }
}
