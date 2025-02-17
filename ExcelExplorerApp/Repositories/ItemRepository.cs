using ExcelExplorerApp.Data;
using ExcelExplorerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelExplorerApp.Repositories
{
    public class ItemRepository: IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllItemAsync()
        {
            return await _context.Items.ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }
        public async Task AddItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if(item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveAllAsync()
        {
            var items = await _context.Items.ToListAsync();
            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}