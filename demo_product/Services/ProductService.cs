using AutoMapper;
using demo_product.Data;
using demo_product.Entity;
using demo_product.Interface;
using demo_product.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace demo_product.Services
{
    public class ProductService : IProductRespository
    {
        public static int PAGE_SIZE { get; set; } = 6;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Thêm
        public async Task<int> AddProductAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct.idProduct;
        }
        // Xóa
        public async Task<bool> DeleteProductAsync(int id)
        {
            var deleteProduct = _context.Products.SingleOrDefault(p => p.idProduct == id);
            if (deleteProduct == null)
            {
                return false; // Sản phẩm không tồn tại
            }
            _context.Products.Remove(deleteProduct);
            await _context.SaveChangesAsync();
            return true; // Xóa thành công
        }
        // Lấy tất cả
        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var product = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(product);
        }
        // Lấy ra 1
        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        // Sửa
        public async Task<bool> UpdateProductAsync(int id, ProductModel model)
        {
            var updateProduct = await _context.Products.FindAsync(id);
            if(updateProduct == null)
            {
                return false; // Sản phẩm không tồn tại
            }
            // Map dữ liệu từ model vào updateProduct
            _mapper.Map(model, updateProduct);
            // Cập nhật
            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();
            return true; // Cập nhật thành công
        }
        // Search
        public List<ProductModel> GetSearchProduct(string? search, string? sortBy, int page = 1)
        {
            var allPRoduct = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allPRoduct = allPRoduct.Where(w => (w.nameProduct != null ? w.nameProduct : "").Contains(search));
            }

            allPRoduct = allPRoduct.OrderBy(w => w.nameProduct);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "w_desc": allPRoduct = allPRoduct.OrderByDescending(w => w.nameProduct); break;
                    case "p_desc": allPRoduct = allPRoduct.OrderByDescending(w => w.priceProduct); break;
                    case "asc": allPRoduct = allPRoduct.OrderBy(w => w.priceProduct); break;
                }
            }

            allPRoduct = allPRoduct.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var result = allPRoduct.Select(w => new ProductModel
            {
                idProduct = w.idProduct,
                nameProduct = w.nameProduct,
                priceProduct = (double)w.priceProduct,
                quantityProduct = w.quantityProduct,
                imgProduct = w.imgProduct,
                statusProduct = w.statusProduct
            });
            return result.ToList();
        }
    }
}
