using System;
using System.Collections.Generic;
using System.Linq;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlowerStoreAPI.Repositories
{
    public class SqlFlowerRepo : IFlowerRepo
    {
        
        private readonly FlowerContext _context;

           public SqlFlowerRepo(FlowerContext context)
        {
            _context = context;
        }

        public void CreateFlower(Flower flower)
        {
            if(flower == null){
                throw new System.NotImplementedException(nameof(flower));
            }
            
            _context.Flowers.Add(flower);
        }

        public void DeleteFlower(Flower flower)
        {
            if(flower == null)
            {
                throw new ArgumentNullException(nameof(flower));
            }

            _context.Flowers.Remove(flower);
        }

        public async Task<IEnumerable<Flower>> GetAllFlowers()
        {
            return await _context.Flowers.ToListAsync();
        }

        public async Task<Flower> GetFlowerById(int id)
        {
            return await _context.Flowers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFlower(Flower flower)
        {
            //nothing
        }
    }
}