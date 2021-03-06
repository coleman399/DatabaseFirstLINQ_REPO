using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            //BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users;
            var numberOfUsers = users.ToList().Count();
            Console.WriteLine(numberOfUsers);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;

            foreach (Product product in products)
            {
                if (product.Price > 150)
                {
                    Console.WriteLine("Product Name: " + product.Name + "\nProduct Price: " + product.Price.ToString("C") + "\n-------------------------");
                }
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products.ToList();

            Console.WriteLine("Product names that contain the letter s:");
            foreach (Product product in products)
            { 
                if (product.Name.Contains("s"))
                {
                    Console.WriteLine(product.Name);
                }
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users.ToList();
            

            Console.WriteLine("Users who have registered before 2016:");
            DateTime dateTime = DateTime.Parse("12/30/2015");
            foreach(User user in users)
            {
                if (user.RegistrationDate < dateTime)
                {
                    Console.WriteLine(user.Email + "Registered on " + user.RegistrationDate);
                }
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users.ToList();

            Console.WriteLine("Users who have registered after 2016 and before 2018");
            DateTime dateTime = DateTime.Parse("01/01/2017");
            DateTime dateTime1 = DateTime.Parse("12/31/2017");
            foreach(User user in users)
            {
                if (user.RegistrationDate < dateTime1 && user.RegistrationDate > dateTime)
                {
                    Console.WriteLine(user.Email + " Registered on " + user.RegistrationDate);
                }
            }


        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var customerShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "afton@gmail.com").Include(sc => sc.Product);

            foreach (ShoppingCart product in customerShoppingCart)
            {
                Console.WriteLine($"Name: {product.Product.Name} Price: {product.Product.Price} Quantity: {product.Quantity}");
            }
         }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var customerShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc =>sc.User.Email == "oda@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();

            Console.WriteLine($"Total price of products: ${customerShoppingCart}");

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employee = _context.UserRoles.Include(ur => ur.User).Where(ur => ur.RoleId == 2).Select(ur => ur.User).ToList();

            var cart = _context.ShoppingCarts.Include(ur => ur.User.UserRoles).Select(sc => new { sc.User, sc.Product, sc.Quantity, sc.User.UserRoles }).ToList();

            foreach (var record in cart)
            {
                if (employee.Contains(record.User))
                {
                    Console.WriteLine($"Email: {record.User.Email} Product Name: {record.Product.Name} Price: {record.Product.Price} Quantity: {record.Quantity}");
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        // run 11 and 12 first then 13 then 14 then 15

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.

            Product newProduct = new Product()
            {
                Name = "Tonka Truck",
                Description = "The Cool new toy from the West",
                Price = 100
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Tonka Truck").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();

        }
        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var item = _context.Products.Where(p => p.Name == "Tonka Truck").SingleOrDefault();
            item.Price = 200;
            _context.Products.Update(item);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.

            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            Console.WriteLine($"Deleted role! ");
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.

            var deleteUser = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(deleteUser);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
                bool userVerify = false;
                string userEmail = "";
                string userPassword = "";
            do
            {
                Console.WriteLine("Please Enter your Email:");
                userEmail = Console.ReadLine();
                Console.WriteLine("Please Enter your Password:");
                userPassword = Console.ReadLine();
                var user = _context.Users.Where(ur => ur.Email == userEmail && ur.Password == userPassword).FirstOrDefault();
                if (user != null)
                {
                    userVerify = true;
                    Console.WriteLine("User has been verified");

                }
                else
                {
                    Console.WriteLine("You have a Unverified Email and Password, please Try Again!\n");
                }

            } while (userVerify == false);
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var aftonShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "afton@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();
            var bibiShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "bibi@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();
            var janettShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "janett@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();
            var garyShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "gary@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();
            var mikeShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == "mike@gmail.com").Include(sc => sc.Product).Select(sc => sc.Product.Price).Sum();
            var totalOfTotals = aftonShoppingCart + bibiShoppingCart + janettShoppingCart + garyShoppingCart + mikeShoppingCart;

            Console.WriteLine($"The total price of products in Afton's shopping cart is: ${aftonShoppingCart}");
            Console.WriteLine($"The total price of products in Bibi's shopping cart is: ${bibiShoppingCart}");
            Console.WriteLine($"The total price of products in Janett's shopping cart is: ${janettShoppingCart}");
            Console.WriteLine($"The total price of products in Gary's shopping cart is: ${garyShoppingCart}");
            Console.WriteLine($"The total price of products in Mike's shopping cart is: ${mikeShoppingCart}");
            Console.WriteLine($"The total price of the products in every user's shopping cart is: ${totalOfTotals}");




        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials
            runEcommerce();

            void runEcommerce()
            {
                string userEmail = signIn();
                operateEcommerce(userEmail);
            }

            

        }
        public string signIn()
            {
                bool userVerify = false;
                string userEmail = "";
                string userPassword = "";
                do
                {
                    Console.WriteLine("Please Enter your Email:\n");
                    userEmail = Console.ReadLine();
                    Console.WriteLine("Please Enter your Password:\n");
                    userPassword = Console.ReadLine();
                    var user = _context.Users.Where(ur => ur.Email == userEmail && ur.Password == userPassword).FirstOrDefault();
                    if (user != null)
                    {
                        userVerify = true;
                    }
                    else
                    {
                        Console.WriteLine("You have a Unverified Email and Password, please Try Again!\n");
                    }

                }while (userVerify== false);

            return userEmail;

            }

        public void operateEcommerce(string userEmail)
        {
            string userInput = "";
                do
                {
                    Console.WriteLine("What would you like to do?:\nView the Products in your Cart?: Enter 1\nView all the optional Products you can buy?: Enter 2\nSign Out: Enter 3\n");
                    userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            displayProducts(userEmail);
                            break;
                        case "2":
                            addItemToCart(userEmail);
                            break;
                        default:
                            break;
                    }

                }while (userInput != "3");

                Console.WriteLine("\n GoodBye \n");

        }

        private void displayProducts( string userEmail)
        {

            var customerShoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.User.Email == userEmail).Include(sc => sc.Product);
            Console.WriteLine("\n\nEverything in your cart currently consist of:\n\n");
            foreach(ShoppingCart product in customerShoppingCart)
            {
                
                Console.WriteLine($"The Item ID is: {product.ProductId}\nProduct: {product.Product.Name}\nCost: ${product.Product.Price}.\n\n");
            }

            
            string userInput = "";

            do
            {
                Console.WriteLine("What would you like to do?\n Remove a Item?: Enter 1\nAdd a Item?: Enter 2\nContinue?: Enter 3\n");
                userInput= Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        removeItemFromCart(userEmail);
                        break;
                    case "2":
                        addItemToCart(userEmail);
                        break;
                    default: break;
                }
            }while (userInput != "3");

        }

        public void removeItemFromCart(string userEmail)
        {
            Console.WriteLine("Please enter the item ID of the item you want to remove.: \n");
            string userInput = Console.ReadLine();
            int itemID = Int32.Parse(userInput);

            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == userEmail);
            var item = shoppingCartProducts.Where(sc => sc.ProductId == itemID).FirstOrDefault();
            _context.ShoppingCarts.Remove(item);
            _context.SaveChanges();
            Console.WriteLine($"The item {item.Product.Name} with ID: {item.ProductId} was Deleted from your Cart!");

        }

        public void addItemToCart(string userEmail)
        {
            Console.WriteLine("\n\nList of Products are as follows:\n");
            var productsList = _context.Products;
            foreach (Product product in productsList)
            {
                Console.WriteLine($"Product ID: {product.Id}\n Product Name: {product.Name}\n Description: {product.Description}\n Cost: ${product.Price}\n\n");
            }


            Console.WriteLine("Please enter the ID number of the item you wish to enter the cart. ");
            var userInput = Int32.Parse(Console.ReadLine());
            var newItem = _context.Products.Where(p => p.Id == userInput).SingleOrDefault();
            
            var userId = _context.Users.Where(u => u.Email == userEmail).Select(u => u.Id).SingleOrDefault();
            var currentItem = _context.ShoppingCarts.Where(sc => sc.UserId == userId && sc.ProductId == newItem.Id).SingleOrDefault();

            if(newItem == null)
            {
                Console.WriteLine("You input a wrong number.\nTry Again!\n\n");
                addItemToCart(userEmail);
            }
            else if(currentItem != null)
            {

                currentItem.Quantity++;
                _context.ShoppingCarts.Update(currentItem);
                _context.SaveChanges();
                Console.WriteLine($"{currentItem.Product.Name} is already in your cart, so we added another to your cart!\n");
            }
            else
            {
                
                ShoppingCart newShoppingCart = new ShoppingCart()
                {
                    UserId = userId,
                    ProductId = newItem.Id
                };
                _context.ShoppingCarts.Add(newShoppingCart);
                _context.SaveChanges();
                Console.WriteLine($"{newItem.Name} was added to your Shooping Cart.");
            }


        }

    }
}
