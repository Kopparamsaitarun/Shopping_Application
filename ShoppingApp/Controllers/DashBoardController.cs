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
    private readonly IWebHostEnvironment _hostEnvironment;



    public DashboardController(IDashboardRepository dashboardRepository, IWebHostEnvironment hostEnvironment)
    {
        _dashboardRepository = dashboardRepository;
        _hostEnvironment = hostEnvironment;
    }
    
    

    [HttpGet("GetProduct")]
    public async Task<IActionResult> GetProduct()
    {
        try
        {
            List<Productlist> productlist = new List<Productlist>();
            IEnumerable<Productlist> products = productlist;
            products = _dashboardRepository.GetAllProduct();
            return View("~/Views/Dashboard/Product.cshtml",products);
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }

    [HttpGet("CreateProductpage")]
    public IActionResult CreateProductpage()
    {
        try
        {
            return View();
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }

    }

    [HttpPost("CreateProduct")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductlistDTO model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = UploadedFile(model);

            ProductlistModel product = new ProductlistModel  //change t
            {
                ProductName = model.ProductName.ToUpper(),
                ProductDiscription = model.ProductDiscription.ToUpper(),

                ProductPrice = model.ProductPrice,
                ProductImage = uniqueFileName,
            };
            _dashboardRepository.InsertProduct(product);
            GetProduct();
        }
        return View("~/Views/Dashboard/Product.cshtml");
    }

    private string UploadedFile(ProductlistDTO model)
    {
        string uniqueFileName = null;

        if (model.ProductImage != null)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ProductImage.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
    }

    [HttpGet]
    public IActionResult AddToCart(long id)
    {
        if (ModelState.IsValid)
        {
            _dashboardRepository.AddtoCart(id);
            GetProduct();

        }
        return View("~/Views/Dashboard/Product.cshtml");
    }

    //[Authorize(Roles = "Admin")]
    [HttpGet("DeleteProduct")]
    public IActionResult DeleteProduct(long id)
    {
        try
        {
            _dashboardRepository.DeleteProduct(id);
            GetProduct();
            return View("~/Views/Dashboard/Product.cshtml");
        }
        catch (Exception exception)
        {
            return BadRequest(new { success = false, exception.Message });
        }
    }


    [HttpGet("GetProductList")]
    public IActionResult GetProductList(long id)
    {
        Productlist productlist = new Productlist();

        if (ModelState.IsValid)
        {
           productlist= _dashboardRepository.GetProduct(id);

        }
        return View("~/Views/Dashboard/Productlst.cshtml", productlist);
    }
}