using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelExplorerApp.Models;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelExplorerApp.Data;

namespace ExcelExplorerApp.Controllers
{
    public class ItemController: Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync();
            if (items == null || !items.Any())
            {
                ViewData["Message"] = "No items found in the database.";
            }
            return View(items);
        }

        public IActionResult Upload()
        {
            return View("~/Views/Item/Upload.cshtml");
        }

        //POST: Item/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var items = _context.Items.ToList();
                _context.Items.RemoveRange(items);
                await _context.SaveChangesAsync();

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var item = new Item
                            {
                                Id = int.Parse(worksheet.Cells[row, 1].Text),
                                Title = worksheet.Cells[row, 2].Text,
                                Dept = worksheet.Cells[row, 3].Text,
                                Division = worksheet.Cells[row, 4].Text,
                                BadgeType = worksheet.Cells[row, 6].Text
                            };

                            string badgeTypeString = worksheet.Cells[row, 5].Text;
                            if (int.TryParse(badgeTypeString, out int badgeTypeID))
                            {
                                item.BadgeTypeID = badgeTypeID;
                            }
                            else
                            {
                                item.BadgeTypeID = 0;
                                Console.WriteLine($"Invalid BadgeTypeID at row {row}: {badgeTypeString}");
                            }

                            _context.Items.Add(item);
                        }
                        await _context.SaveChangesAsync();
                        ViewData["Message"] = "Data successfully uploaded.";
                    }
                }
            }
            else
            {
                ViewData["Message"] = "Please select a file to upload.";
            }

            return RedirectToAction("Index", "Item");
        }


        //GET: Item/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        //POST: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Dept,Division,BadgeTypeID,BadgeType")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        //GET: Item/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST: Item/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
