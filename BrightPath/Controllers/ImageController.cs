using BrightPath.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BrightPath.Controllers
{
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        //[Route("addimage")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(AddImageViewModel addImageViewModel)
        {
            /// TODO: decide if the path is the one you want
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
            foreach (var file in addImageViewModel.Files)
            {
                if (file.Length > 0)
                {
                    //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            //  TODO: add the file path to the article it belongs to, so  you can retrieve it when the article is clicked/shown
            return View();
        }

    }
}