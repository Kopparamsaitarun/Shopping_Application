using Domain.Model.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Dashboard;
using ShoppingApp.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[Route("[controller]")]
public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;
    //Provides information about the web hosting environment an application is running in
    private readonly IWebHostEnvironment _hostEnvironment;

    public DashboardController(IDashboardRepository dashboardRepository, IWebHostEnvironment hostEnvironment)
    {
        _dashboardRepository = dashboardRepository;
        _hostEnvironment = hostEnvironment;
    }
      /// <summary>
      /// Get All the product to display on dashboard 
      /// </summary>
      /// <returns></returns>
    [HttpGet("GetAllProduct")]
    public IActionResult GetAllProduct()
    {
        try
        {
            List<Productlist> productlist = new List<Productlist>();
            IEnumerable<Productlist> products = productlist;
            products = _dashboardRepository.GetAllProduct();

            return View("~/Views/Dashboard/Product.cshtml", products);


        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }
    /// <summary>
    /// Authorize person mostly admin will get access to that page returning the view of createproduct 
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("CreateProduct")]
    public IActionResult CreateProduct()
    {
        try
        {
            return  View();
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }

    }
    /// <summary>
    /// Authorize person will add the product for that calling dashboardrepository method
    /// </summary>
    /// We convert the image which is in iform file to the string format to store in database
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("CreateProduct")]
    [ValidateAntiForgeryToken]
    public ActionResult CreateProduct(ProductlistDTO model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //Covert the file format from inform file to string 
                string uniqueFileName = UploadedFile(model);

                ProductlistModel product = new ProductlistModel  
                {
                    ProductName = model.ProductName.ToUpper(),
                    ProductDiscription = model.ProductDiscription.ToUpper(),

                    ProductPrice = model.ProductPrice,
                    ProductImage = uniqueFileName,
                };
                _dashboardRepository.InsertProduct(product);
                GetAllProduct();
            }
            return View("~/Views/Dashboard/Product.cshtml");
        }
        catch(Exception exception) 
        {
            return BadRequest(new { success = false, exception.Message });

        }
    }
    /// <summary>
    /// Create 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private string UploadedFile(ProductlistDTO model)
    {
        string uniqueFileName = null;

        if (model.ProductImage != null)
        {
            //Combine running hostaname and folder
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            //Guid=>A GUID is a 128-bit integer (16 bytes) that can be used across all computers and networks wherever a unique identifier is required
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ProductImage.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
    }

    /// <summary>
    /// Add to cart the product in cart  for that calling dashboardrepository
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("AddToCart")]
    public IActionResult AddToCart(long id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _dashboardRepository.AddtoCart(id);
                GetAllProduct();

            }
            return View("~/Views/Dashboard/Product.cshtml");
        }
        catch (Exception exception) 
        {
            return BadRequest(new { success = false, exception.Message });

        }
    }
    /// <summary>
    /// Authorize person can delete the product for that call dashboard repository
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("DeleteProduct")]
    public IActionResult DeleteProduct(long id)
    {
        try
        {
            _dashboardRepository.DeleteProduct(id);
            GetAllProduct();
            return View("~/Views/Dashboard/Product.cshtml");
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }

    /// <summary>
    /// Get particular product from the dashboard for that call dashboardrepository by passing id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetProduct")]
    public IActionResult GetProduct(long id)
    {
        try
        {
            Productlist productlist = new Productlist();

            if (ModelState.IsValid)
            {
                productlist = _dashboardRepository.GetProduct(id);

            }
            return View("~/Views/Dashboard/Productlst.cshtml", productlist);
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });

        }
    }

    /// <summary>
    /// Sort the product using price range  Low to high
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductLowHigh")]
    public IActionResult GetProductLowHigh()
    {
        try
        {
            List<Productlist> productlist = new List<Productlist>();
            IEnumerable<Productlist> products = productlist;
            products = _dashboardRepository.GetProductLowHigh();

            return View("~/Views/Dashboard/Product.cshtml", products);


        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }

    /// <summary>
    /// Sort the product using price range high to low
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductHighLow")]
    public IActionResult GetProductHighLow()
    {
        try
        {
            List<Productlist> productlist = new List<Productlist>();
            IEnumerable<Productlist> products = productlist;
            products = _dashboardRepository.GetProductHighLow();

            return View("~/Views/Dashboard/Product.cshtml", products);


        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }

    /// <summary>
    /// Search the product by the product name and product description
    /// </summary>
    /// <param name="searchString"></param>
    /// <returns></returns>

	[HttpGet("SearchProduct")]
	public IActionResult SearchProduct(string searchString)
	{
		try
		{
			List<Productlist> productlist = new List<Productlist>();
			IEnumerable<Productlist> products = productlist;
			products = _dashboardRepository.GetSearchItems(searchString);
			return View("~/Views/Dashboard/Product.cshtml", products);

		}
		catch (Exception exception)
		{
			return BadRequest(new { success = false, exception.Message });
		}
	}

}