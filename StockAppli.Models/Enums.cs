namespace StockAppli.Models
{
    public class Enums
    {
        public enum UserRole
        {
            Owner = 0,
            Admin = 1,
            Collaborator = 2
        }
        public enum Permission
        {
            UserRead = 0,
            UserCreate = 1,
            UserDelete = 2, 
            UserUpdate = 3,

            CompanyRead = 4,
            CompanyCreate = 5,
            CompanyDelete = 6,
            CompanyUpdate = 7,

            ProductRead = 8,
            ProductCreate = 9,
            ProductDelete = 10,
            ProductUpdate = 11,

            ClientRead = 12,
            ClientCreate = 13,
            ClientDelete = 14,
            ClientUpdate = 15,

            SupplierRead = 16,
            SupplierCreate = 17,
            SupplierDelete = 18,
            SupplierUpdate = 19,

            CategoryRead = 20,
            CategoryCreate = 21,
            CategoryDelete = 22,
            CategoryUpdate = 23,
        }
    }
}
