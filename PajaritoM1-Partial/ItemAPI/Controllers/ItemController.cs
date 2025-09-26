using Microsoft.AspNetCore.Mvc;
using ItemDataLibrary.Model;

namespace ItemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<ItemModel> items = new List<ItemModel>
        {
            new ItemModel { Name = "S25 Ultra", Code = "SNU1001", Brand = "Samsung", UnitPrice = 79900m, Quantity = 12 },
            new ItemModel { Name = "RTX 4070 Graphics Card", Code = "GFX4070", Brand = "NVIDIA", UnitPrice = 45000m, Quantity = 8 },
            new ItemModel { Name = "Corsair Vengeance 16GB", Code = "MEMV16", Brand = "Corsair", UnitPrice = 3200m, Quantity = 20 },
            new ItemModel { Name = "Samsung EVO SSD 1TB", Code = "SSD1000", Brand = "Samsung", UnitPrice = 8500m, Quantity = 15 },
            new ItemModel { Name = "Logitech G Pro X Headset", Code = "HST100", Brand = "Logitech", UnitPrice = 5200m, Quantity = 10 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ItemModel>> GetAllItems() => Ok(items);

        [HttpGet("{code}")]
        public ActionResult<ItemModel> GetItem(string code)
        {
            var item = items.FirstOrDefault(i => i.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ItemModel> AddItem(ItemModel newItem)
        {
            var exists = items.Any(i => i.Code.Equals(newItem.Code, StringComparison.OrdinalIgnoreCase));
            if (exists) return Conflict("Item with this code already exists.");

            items.Add(newItem);
            return CreatedAtAction(nameof(GetItem), new { code = newItem.Code }, newItem);
        }

        [HttpPut("{code}")]
        public ActionResult UpdateItem(string code, ItemModel updatedItem)
        {
            var item = items.FirstOrDefault(i => i.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (item == null) return NotFound();

            item.Name = updatedItem.Name;
            item.Brand = updatedItem.Brand;
            item.UnitPrice = updatedItem.UnitPrice;
            item.Quantity = updatedItem.Quantity;

            return NoContent();
        }

        [HttpDelete("{code}")]
        public ActionResult DeleteItem(string code)
        {
            var item = items.FirstOrDefault(i => i.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (item == null) return NotFound();

            items.Remove(item);
            return NoContent();
        }
    }
}