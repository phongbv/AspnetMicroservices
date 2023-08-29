using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = CreateConnection();
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
        ("SELECT * FROM COUPON WHERE ProductName = @ProductName",
            new { ProductName = productName });
        if (coupon is null)
        {
            return new()
            {
                ProductName = "No discount",
                Amount = 0,
                Description = "No Discount Desc"
            };
        }

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var conn = CreateConnection();
        var affected = await conn.ExecuteAsync(
            "INSERT INTO Coupon(ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new { coupon.ProductName, coupon.Description, coupon.Amount }
        );
        return affected != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var conn = CreateConnection();
        var affected = await conn.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });
        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var conn = CreateConnection();
        var affected = await conn.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });
        return affected != 0;
    }

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    }
}