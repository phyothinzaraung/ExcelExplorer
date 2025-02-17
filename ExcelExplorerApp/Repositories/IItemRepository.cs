using ExcelExplorerApp.Models;

namespace ExcelExplorerApp.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task RemoveAllAsync();

    }
}